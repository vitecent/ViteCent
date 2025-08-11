#region

// 引入 MediatR 用于实现中介者模式
using AutoMapper;
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Application;

// 引入基础数据传输对象

// 引入基础日志数据传输对象
using ViteCent.Auth.Data.BaseLogs;

// 引入用户信息相关的数据传输对象
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;

// 引入核心
using ViteCent.Core;
using ViteCent.Core.Cache;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 绑定指纹接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mapper">对象映射器，用于参数和模型对象之间的转换</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 API 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseUser")]
public class Finger(
    // 注入日志记录器
    ILogger<Finger> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入缓存器
    IBaseCache cache,
    // 注入对象映射器
    IMapper mapper,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<FingerArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 绑定指纹
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>绑定指纹结果</returns>
    [HttpPost] // 标记为POST请求
    // 设置路由名称
    [Route("Finger")]
    public override async Task<BaseResult> InvokeAsync(FingerArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Finger");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        // 创建日志参数对象，用于记录操作日志
        var logsArgs = new AddBaseLogsArgs()
        {
            CompanyId = user?.Company?.Id ?? string.Empty,
            CompanyName = user?.Company?.Name ?? string.Empty,
            DepartmentId = user?.Department?.Id ?? string.Empty,
            DepartmentName = user?.Department?.Name ?? string.Empty,
            SystemId = string.Empty,
            SystemName = "Auth",
            ResourceId = string.Empty,
            ResourceName = "BaseUser",
            OperationId = string.Empty,
            OperationName = "Finger",
            Description = "绑定指纹",
            Args = args.ToJson()
        };

        // 创建数据验证器
        var validator = new FingerValidator();

        // 验证用户信息的有效性
        var check = await validator.ValidateAsync(args, cancellationToken);

        // 如果验证失败，返回错误信息
        if (!check.IsValid)
        {
            // 记录失败操作日志
            await mediator.LogError(logsArgs, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty, cancellationToken);

            // 返回操作结果
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(args.CompanyId))
            args.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(args.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(args.DepartmentId))
            args.DepartmentId = departmentId;

        var hasDepartment = await mediator.CheckDepartment(args.CompanyId, args.DepartmentId);

        if (!hasDepartment.Success)
            return hasDepartment;

        var positionId = user?.Position?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(args.PositionId))
            args.PositionId = positionId;

        var hasPosition = await mediator.CheckPosition(args.CompanyId, args.PositionId);

        if (!hasPosition.Success)
            return hasPosition;

        if (!cache.HasKey("RegisterFinger"))
            return new BaseResult(500, "请先录入指纹信息");

        var finger = cache.GetString<string>("RegisterFinger");

        if (string.IsNullOrWhiteSpace(finger))
            return new BaseResult(500, "指纹信息不存在");

        var getArgs = mapper.Map<GetBaseUserEntityArgs>(args);

        var entity = await mediator.Send(getArgs, cancellationToken);

        if (entity is null)
            return new BaseResult(500, "用户信息不存在");

        entity.Finger = finger;
        entity.Updater = user?.Name ?? string.Empty;
        entity.UpdateTime = DateTime.Now;
        entity.Version = DateTime.Now;

        var result = await mediator.Send(entity, cancellationToken);

        return result;
    }
}
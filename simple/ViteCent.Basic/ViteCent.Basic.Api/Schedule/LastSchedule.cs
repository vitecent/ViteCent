#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象
using ViteCent.Basic.Application;

// 引入基础日志数据传输对象
using ViteCent.Basic.Data.BaseLogs;

// 引入排班信息相关的数据传输对象
using ViteCent.Basic.Data.Schedule;

// 引入核心
using ViteCent.Core;

// 引入核心数据缓存
using ViteCent.Core.Cache;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 下班打卡接口
/// </summary>
/// <param name="logger"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="mediator"></param>
/// <param name="cache"></param>
// 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("Schedule")]
public class LastSchedule(
    // 注入日志记录器
    ILogger<FirstSchedule> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者接口
    IMediator mediator,
    // 注入缓存接口
    IBaseCache cache)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<LastScheduleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 下班打卡
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    // 标记为POST请求
    [HttpPost]
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Edit" })]
    // 设置路由名称
    [Route("Last")]
    public override async Task<BaseResult> InvokeAsync(LastScheduleArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.LastSchedule");

        // 设置公司标识
        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        // 设置部门标识
        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;

        // 创建取消令牌，用于支持异步操作的取消
        var cancellationToken = new CancellationToken();

        // 创建日志参数对象，用于记录操作日志
        var logsArgs = new AddBaseLogsArgs()
        {
            CompanyId = user?.Company?.Id ?? string.Empty,
            CompanyName = user?.Company?.Name ?? string.Empty,
            DepartmentId = user?.Department?.Id ?? string.Empty,
            DepartmentName = user?.Department?.Name ?? string.Empty,
            SystemId = string.Empty,
            SystemName = "Basic",
            ResourceId = string.Empty,
            ResourceName = "Schedule",
            OperationId = string.Empty,
            OperationName = "Last",
            Description = "下班打卡",
            Args = args.ToJson()
        };

        // 创建数据验证器
        var validator = new LastScheduleValidator();

        // 验证排班信息的有效性
        var check = await validator.ValidateAsync(args, cancellationToken);

        // 如果验证失败，返回错误信息
        if (!check.IsValid)
        {
            // 记录失败操作日志
            await mediator.LogError(logsArgs, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty, cancellationToken);

            // 返回操作结果
            return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);
        }

        var checkCompany = user?.CheckCompanyId(args.CompanyId);

        if (checkCompany is not null && !checkCompany.Success)
            return checkCompany;

        if (args.Model == (int)ModelEnum.Finger)
        {
            if (!cache.HasKey("Finger"))
                return new BaseResult(500, "指纹信息不存在");

            var userFinger = cache.GetString<UserFinger>("Finger");

            if (userFinger is null)
                return new BaseResult(500, "指纹信息不存在");

            if (userFinger.UserId != args.UserId)
                return new BaseResult(500, "指纹信息不匹配");

            cache.DeleteKey("Finger");
        }

        // 通过中介者发送命令并返回结果
        var result = await mediator.Send(args, cancellationToken);

        // 记录失败操作日志
        if (!result.Success)
            await mediator.LogError(logsArgs, result.Message, cancellationToken);

        // 记录成功操作日志
        await mediator.LogSuccess(logsArgs, cancellationToken);

        // 返回操作结果
        return result;
    }
}
#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础日志数据传输对象
using ViteCent.Auth.Data.BaseLogs;

// 引入用户信息相关的数据传输对象
using ViteCent.Auth.Data.BaseUser;

// 引入核心
using ViteCent.Core;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 初始化接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
// 标记为 API 接口
[ApiController]
// 设置路由前缀
[Route("BaseUser")]
public class Initialize(
    // 注入日志记录器
    ILogger<Initialize> logger,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<InitializeArgs, BaseResult>
{
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    // 标记为 POST 请求
    [HttpPost]
    // 设置路由名称
    [Route("Initialize")]
    public override async Task<BaseResult> InvokeAsync(InitializeArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Initialize");

        if (string.IsNullOrEmpty(args.Username) && !string.IsNullOrWhiteSpace(args.RealName))
            args.Username = args.RealName.GetPinYin().ToCamelCase();

        if (string.IsNullOrEmpty(args.Password))
            args.Password = BaseConst.DefaultPassword;

        // 创建取消令牌，用于支持异步操作的取消
        var cancellationToken = new CancellationToken();

        // 创建日志参数对象，用于记录操作日志
        var logsArgs = new AddBaseLogsArgs()
        {
            CompanyId = string.Empty,
            CompanyName = string.Empty,
            DepartmentId = string.Empty,
            DepartmentName = string.Empty,
            SystemId = string.Empty,
            SystemName = "Auth",
            ResourceId = string.Empty,
            ResourceName = "BaseUser",
            OperationId = string.Empty,
            OperationName = "Initialize",
            Description = "初始化",
            Args = args.ToJson()
        };

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
/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 Asp.Net Core Mvc 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入用户角色相关的数据参数
using ViteCent.Auth.Data.BaseUserRole;

// 引入基础日志数据参数
using ViteCent.Auth.Data.BaseLogs;

// 引入核心
using ViteCent.Core;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion 引入命名空间

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// 删除用户角色接口
/// </summary>
/// <remarks>
/// 该接口负责处理删除用户角色的请求，主要功能包括：
/// 1. 验证用户登录状态
/// 2. 验证用户权限
/// 3. 处理删除用户角色的请求
/// 4. 返回操作结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 Api 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseUserRole")]
public partial class DeleteBaseUserRole(
    // 注入日志记录器
    ILogger<DeleteBaseUserRole> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<DeleteBaseUserRoleArgs, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 删除用户角色
    /// </summary>
    /// <remarks>
    /// 该方法主要完成以下功能：
    /// 1. 记录方法调用日志，便于追踪和调试
    /// 2. 验证参数有效性
    /// 3. 通过中介者发送删除命令
    /// 4. 返回操作结果
    /// </remarks>
    /// <param name="args">删除用户角色的参数</param>
    /// <returns>返回删除操作的结果</returns>
    [HttpPost] // 标记为 Post 请求
    // 使用权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Delete" })]
    // 设置路由名称
    [Route("Delete")]
    public override async Task<BaseResult> InvokeAsync(DeleteBaseUserRoleArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.DeleteBaseUserRole");

        // 重写调用方法
        OverrideInvoke(args, user);

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
            ResourceName = "BaseUserRole",
            OperationId = string.Empty,
            OperationName = "Delete",
            Description = "删除用户角色",
            Args = args.ToJson()
        };

        // 验证参数有效性
        if (args is null)
            return new BaseResult(500, "参数不能为空");

        // 通过中介者发送删除命令并返回结果
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
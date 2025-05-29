/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入基础数据传输对象
using ViteCent.Auth.Application;

// 引入用户角色相关的数据传输对象
using ViteCent.Auth.Data.BaseUserRole;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心枚举类型
using ViteCent.Core.Enums;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUserRole;

/// <summary>
/// 获取用户角色接口
/// </summary>
/// <remarks>
/// 该接口负责处理获取用户角色的请求，主要功能包括：
/// 1. 验证用户登录状态
/// 2. 验证用户权限
/// 3. 处理获取用户角色的请求
/// 4. 返回用户角色数据
/// </remarks>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">中介者接口，用于发送查询请求</param>
// 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseUserRole")]
public class GetBaseUserRole(
    // 注入日志记录器
    ILogger<GetBaseUserRole> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<GetBaseUserRoleArgs, DataResult<BaseUserRoleResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 获取用户角色
    /// </summary>
    /// <remarks>
    /// 该方法实现了以下功能：
    /// 1. 记录方法调用日志
    /// 2. 验证请求参数的有效性
    /// 3. 通过中介者发送查询请求
    /// 4. 返回查询结果
    /// </remarks>
    /// <param name="args">查询参数，包含获取用户角色所需的条件</param>
    /// <returns>返回包含用户角色的数据结果对象</returns>
    // 标记为POST请求
    [HttpPost]
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseUserRole", "Get" })]
    // 设置路由名称
    [Route("Get")]
    public override async Task<DataResult<BaseUserRoleResult>> InvokeAsync(GetBaseUserRoleArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUserRole.GetBaseUserRole");

        // 验证参数是否为空，确保请求参数的有效性
        if (args is null)
            return new DataResult<BaseUserRoleResult>(500, "参数不能为空");

        // 如果用户不是超级管理员，则验证公司标识是否为空
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                return new DataResult<BaseUserRoleResult>(500, "公司标识不能为空");

        // 验证用户角色的有效性
        var check = user.CheckCompanyId(args.CompanyId);

        // 如果验证失败，返回错误信息
        if (check is not null && !check.Success)
            return new DataResult<BaseUserRoleResult>(check.Code, check.Message);

        // 如果用户不是超级管理员，则验证部门标识是否为空
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                return new DataResult<BaseUserRoleResult>(500, "部门标识不能为空");

        // 如果用户不是超级管理员，则验证角色标识是否为空
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.RoleId))
                return new DataResult<BaseUserRoleResult>(500, "角色标识不能为空");

        // 如果用户不是超级管理员，则验证用户标识是否为空
        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.UserId))
                return new DataResult<BaseUserRoleResult>(500, "用户标识不能为空");

        // 创建取消令牌，用于支持异步操作的取消
        var cancellationToken = new CancellationToken();

        // 通过中介者发送查询命令并返回结果
        return await mediator.Send(args, cancellationToken);
    }
}
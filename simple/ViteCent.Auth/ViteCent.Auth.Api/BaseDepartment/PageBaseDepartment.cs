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

// 引入部门信息相关的数据传输对象
using ViteCent.Auth.Data.BaseDepartment;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDepartment;

/// <summary>
/// 部门信息分页查询接口
/// </summary>
/// <remarks>
/// 该接口负责处理分页查询部门信息的请求，主要功能包括：
/// 1. 处理部门信息的分页查询请求
/// 2. 验证用户登录状态
/// 3. 验证用户权限
/// 4. 返回分页查询结果
/// </remarks>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
/// <param name="mediator">中介者接口，用于处理查询请求</param>
 // 标记为API接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseDepartment")]
public class PageBaseDepartment(
    // 注入日志记录器
    ILogger<PageBaseDepartment> logger,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>>
{
    /// <summary>
    /// 部门信息分页查询
    /// </summary>
    /// <param name="args">查询参数，包含分页、筛选等条件</param>
    /// <returns>返回分页查询结果，包含部门信息列表和分页信息</returns>
    /// <remarks>
    /// 该方法实现以下功能：
    /// 1. 记录操作日志
    /// 2. 验证参数有效性
    /// 3. 通过中介者发送查询请求
    /// 4. 返回查询结果
    /// </remarks>
    // 标记为POST请求
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDepartment", "Get" })]
    // 设置路由名称
    [Route("Page")]
    public override async Task<PageResult<BaseDepartmentResult>> InvokeAsync(SearchBaseDepartmentArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDepartment.PageBaseDepartment");

        // 验证参数是否为空，确保请求参数的有效性
        if (args is null)
            return new PageResult<BaseDepartmentResult>(500, "参数不能为空");

        // 创建取消令牌，用于支持异步操作的取消
        var cancellationToken = new CancellationToken();

        // 通过中介者发送分页命令并返回结果
        return await mediator.Send(args, cancellationToken);
    }
}
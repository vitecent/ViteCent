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

// 引入数据库信息相关的数据参数
using ViteCent.Database.Data.BaseDatabase;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion 引入命名空间

namespace ViteCent.Database.Api.BaseDatabase;

/// <summary>
/// 获取数据库信息接口
/// </summary>
/// <remarks>
/// 该接口负责处理获取数据库信息的请求，主要功能包括：
/// 1. 验证用户登录状态
/// 2. 验证用户权限
/// 3. 处理获取数据库信息的请求
/// 4. 返回数据库信息数据
/// </remarks>
/// <param name="logger">日志记录器，用于记录接口的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 Api 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseDatabase")]
public partial class GetBaseDatabase(
    // 注入日志记录器
    ILogger<GetBaseDatabase> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<GetBaseDatabaseArgs, DataResult<BaseDatabaseResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 获取数据库信息
    /// </summary>
    /// <remarks>
    /// 该方法实现了以下功能：
    /// 1. 记录方法调用日志
    /// 2. 验证请求参数的有效性
    /// 3. 通过中介者发送查询请求
    /// 4. 返回查询结果
    /// </remarks>
    /// <param name="args">查询参数，包含获取数据库信息所需的条件</param>
    /// <returns>返回包含数据库信息的数据结果对象</returns>
    [HttpPost] // 标记为 Post 请求
    // 权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Database", "BaseDatabase", "Get" })]
    // 设置路由名称
    [Route("Get")]
    public override async Task<DataResult<BaseDatabaseResult>> InvokeAsync(GetBaseDatabaseArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Database.Api.BaseDatabase.GetBaseDatabase");

        // 重写调用方法
        OverrideInvoke(args, user);

        // 验证参数是否为空，确保请求参数的有效性
        if (args is null)
            return new DataResult<BaseDatabaseResult>(500, "参数不能为空");

        // 创建取消令牌，用于支持操作的取消
        var cancellationToken = new CancellationToken();

        // 通过中介者发送查询命令并返回结果
        return await mediator.Send(args, cancellationToken);
    }
}
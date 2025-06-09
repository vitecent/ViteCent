/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入 ASP.NET Core MVC 核心功能
using Microsoft.AspNetCore.Mvc;

// 引入日志信息相关的数据参数
using ViteCent.Database.Data.BaseLogs;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入核心接口基类
using ViteCent.Core.Web.Api;

// 引入核心过滤器
using ViteCent.Core.Web.Filter;

#endregion 引入命名空间

namespace ViteCent.Database.Api.BaseLogs;

/// <summary>
/// 批量新增日志信息接口
/// </summary>
/// <remarks>
/// 该接口负责处理批量新增日志信息的请求，主要功能包括：
/// 1. 验证用户登录状态
/// 2. 验证用户权限
/// 3. 验证批量新增数据的有效性
/// 4. 处理批量新增日志信息的请求
/// 5. 返回操作结果
/// </remarks>
/// <param name="logger">用于记录接口的操作日志</param>
/// <param name="httpContextAccessor">HTTP上下文访问器，用于获取当前用户信息</param>
/// <param name="mediator">用于发送命令请求</param>
// 标记为 API 接口
[ApiController]
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseLogs")]
public class AddBaseLogsList(
    // 注入日志记录器
    ILogger<AddBaseLogsList> logger,
    // 注入HTTP上下文访问器
    IHttpContextAccessor httpContextAccessor,
    // 注入中介者接口
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseListApi<List<AddBaseLogsArgs>, BaseResult>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private readonly BaseUserInfo user = httpContextAccessor.InitUser();

    /// <summary>
    /// 批量新增日志信息
    /// </summary>
    /// <remarks>
    /// 该方法主要完成以下功能：
    /// 1. 记录方法调用日志，便于追踪和调试
    /// 2. 验证参数有效性
    /// 3. 验证日志信息列表不为空且不重复
    /// 4. 创建取消令牌和数据验证器
    /// 5. 验证每个日志信息的有效性
    /// 6. 通过中介者发送批量新增命令
    /// 7. 返回操作结果
    /// </remarks>
    /// <param name="args">批量新增日志信息的参数</param>
    /// <returns>返回批量新增操作的结果</returns>
    // 标记为 POST 请求
    [HttpPost]
    // 使用权限验证过滤器，验证用户是否有权限访问该接口
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Database", "BaseLogs", "Add" })]
    // 设置路由名称
    [Route("AddList")]
    public override async Task<BaseResult> InvokeAsync(List<AddBaseLogsArgs> args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Database.Api.BaseLogs.AddBaseLogsList");

        // 创建取消令牌
        var cancellationToken = new CancellationToken();

        // 验证参数不为空
        if (args is null)
        {
            // 返回操作结果
            return new BaseResult(500, "参数不能为空");
        }

        // 验证日志信息列表不为空
        if (args.Count == 0)
        {
            // 返回操作结果
            return new BaseResult(500, "日志信息不能为空");
        }

        // 获取去重日志信息数量
        var count = args.Distinct().Count();

        // 验证日志信息不重复
        if (count != args.Count)
        {
            // 返回操作结果
            return new BaseResult(500, "日志信息重复");
        }

        // 创建数据验证器，用于支持操作的取消
        var validator = new BaseLogsValidator();

        // 验证每个日志信息的有效性
        foreach (var item in args)
        {
            // 重写调用方法
            AddBaseLogs.OverrideInvoke(item, user);

            // 验证日志信息的有效性
            var check = await validator.ValidateAsync(item, cancellationToken);

            // 如果验证失败，返回错误信息
            if (!check.IsValid)
            {
                // 返回操作结果
                return new BaseResult(500, check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);
            }
        }

        // 构建请求参数
        var request = new AddBaseLogsListArgs()
        {
            Items = args
        };

        // 通过中介者发送批量新增命令并返回结果
        var result = await mediator.Send(request, cancellationToken);

        // 返回操作结果
        return result;
    }
}
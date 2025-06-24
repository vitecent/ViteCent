#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 获取用户信息接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 API 接口
// 使用登录过滤器，确保用户已登录
[ServiceFilter(typeof(BaseLoginFilter))]
// 设置路由前缀
[Route("BaseUser")]
public class GetUserInfo(
    // 注入日志记录器
    ILogger<GetUserInfo> logger,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<GetUserInfoArgs, DataResult<BaseUserInfo>>
{
    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>用户信息</returns>
    [HttpPost] // 标记为POST请求
    // 设置路由名称
    [Route("GetUserInfo")]
    public override async Task<DataResult<BaseUserInfo>> InvokeAsync(GetUserInfoArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.GetUserInfo");

        // 通过中介者发送命令并返回结果
        return await mediator.Send(args);
    }
}
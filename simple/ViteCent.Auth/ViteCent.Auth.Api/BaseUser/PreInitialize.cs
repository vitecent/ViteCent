#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 预初始化接口
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
[ApiController] // 标记为 API 接口
// 设置路由前缀
[Route("BaseUser")]
public class PreInitialize(
    // 注入日志记录器
    ILogger<PreInitialize> logger,
    // 注入中介者
    IMediator mediator)
    // 继承基类，指定查询参数和返回结果类型
    : BaseApi<PreInitializeArgs, DataResult<PreInitializeResult>>
{
    /// <summary>
    /// 预初始化
    /// </summary>
    /// <param name="args">请求参数</param>
    /// <returns>预初始化结果</returns>
    [HttpPost] // 标记为POST请求
    // 设置路由名称
    [Route("PreInitialize")]
    public override async Task<DataResult<PreInitializeResult>> InvokeAsync(PreInitializeArgs args)
    {
        // 记录方法调用日志，便于追踪和调试
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.PreInitialize");

        // 通过中介者发送命令并返回结果
        return await mediator.Send(args);
    }
}
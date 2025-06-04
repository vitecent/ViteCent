#region

using MediatR;
using System.Security.Claims;
using ViteCent.Auth.Data.BaseLogs;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Api;

/// <summary>
/// </summary>
public static class BaseApi
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="args"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task LogError(this IMediator mediator, AddBaseLogsArgs args, string message, CancellationToken cancellationToken)
    {
        var logger = new BaseLogger(typeof(BaseApi));

        args.Status = (int)YesNoEnum.No;
        args.Description = message;

        try
        {
            await mediator.Send(args, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="args"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static async Task LogSuccess(this IMediator mediator, AddBaseLogsArgs args, CancellationToken cancellationToken)
    {
        var logger = new BaseLogger(typeof(BaseApi));

        args.Status = (int)YesNoEnum.Yes;

        try
        {
            await mediator.Send(args, cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
        }
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    /// <returns></returns>
    public static BaseUserInfo InitUser(this IHttpContextAccessor httpContextAccessor)
    {
        // 创建用户信息对象
        var user = new BaseUserInfo();

        // 获取HTTP上下文
        var context = httpContextAccessor.HttpContext;

        // 从请求头中获取Token
        var token = context?.Request.Headers[BaseConst.Token].ToString() ?? string.Empty;

        // 从Claims中获取用户数据
        var json = context?.User.FindFirstValue(ClaimTypes.UserData);

        // 如果存在用户数据，则反序列化为用户信息对象
        if (!string.IsNullOrWhiteSpace(json))
            user = json.DeJson<BaseUserInfo>();

        // 设置用户Token
        user.Token = token;

        // 返回初始化后的用户信息
        return user;
    }
}
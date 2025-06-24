#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 刷新Token仓储
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
/// <param name="cache">缓存器，用于处理缓存信息</param>
/// <param name="mediator">中介者，用于发送查询请求</param>
public class RefreshToken(
    ILogger<RefreshToken> logger,
    IBaseCache cache,
    IMediator mediator) : IRequestHandler<RefreshTokenArgs, DataResult<RefreshTokenResult>>
{
    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<DataResult<RefreshTokenResult>> Handle(RefreshTokenArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.RefreshToken");

        var loginArgs = new LoginArgs();

        if (cache.HasKey($"User{request.RefreshToken}"))
            loginArgs = cache.GetString<LoginArgs>($"User{request.RefreshToken}");

        var login = await mediator.Send(loginArgs, cancellationToken);

        if (!login.Success)
            return new DataResult<RefreshTokenResult>(login.Code, login.Message);

        var result = new DataResult<RefreshTokenResult>
        {
            Data = new RefreshTokenResult
            {
                Token = login.Data.Token
            }
        };

        return result;
    }
}
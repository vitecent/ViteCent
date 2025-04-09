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
/// <param name="logger"></param>
/// <param name="cache"></param>
/// <param name="mediator"></param>
public class RefreshToken(ILogger<RefreshToken> logger,
    IBaseCache cache,
    IMediator mediator) : IRequestHandler<RefreshTokenArgs, DataResult<RefreshTokenResult>>
{
    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<RefreshTokenResult>> Handle(RefreshTokenArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Application.BaseUser.RefreshToken");

        var loginArgs = new LoginArgs();

        if (cache.HasKey($"User{request.RefreshToken}"))
            loginArgs = cache.GetString<LoginArgs>($"User{request.RefreshToken}");

        var login = await mediator.Send(loginArgs, cancellationToken);

        if (!login.Success)
            return new DataResult<RefreshTokenResult>(login.Code, login.Message);

        var result = new DataResult<RefreshTokenResult>()
        {
            Data = new()
            {
                Token = login.Data.Token,
            }
        };

        return result;
    }
}
#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// 刷新Token接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[Route("BaseUser")]
public class RefreshToken(
    ILogger<RefreshToken> logger,
    IMediator mediator) : BaseApi<RefreshTokenArgs, DataResult<RefreshTokenResult>>
{
    /// <summary>
    /// 刷新Token
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("RefreshToken")]
    public override async Task<DataResult<RefreshTokenResult>> InvokeAsync(RefreshTokenArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.RefreshToken");

        var cancellationToken = new CancellationToken();
        var validator = new RefreshTokenArgsValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (!result.IsValid)
            return new DataResult<RefreshTokenResult>(500,
                result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        return await mediator.Send(args, cancellationToken);
    }
}
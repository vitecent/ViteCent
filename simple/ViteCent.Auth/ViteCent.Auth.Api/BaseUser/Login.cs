#region

using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;

#endregion

namespace ViteCent.Auth.Api.BaseUser;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[Route("BaseUser")]
public class Login(ILogger<Login> logger, IMediator mediator) : BaseApi<LoginArgs, DataResult<LoginResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Login")]
    public override async Task<DataResult<LoginResult>> InvokeAsync(LoginArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Login");

        var cancellationToken = new CancellationToken();
        var validator = new LoginArgsValidator();
        var result = await validator.ValidateAsync(args, cancellationToken);

        if (result.IsValid)
            return await mediator.Send(args, cancellationToken);

        var message = string.Join(",", result.Errors.Select(x => x.ErrorMessage));

        return new DataResult<LoginResult>(500, message);
    }
}
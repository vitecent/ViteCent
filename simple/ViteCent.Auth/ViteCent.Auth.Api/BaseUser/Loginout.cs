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
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseUser")]
public class Loginout(ILogger<Loginout> logger, IMediator mediator) : BaseLoginApi<LoginoutArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("Loginout")]
    public override async Task<BaseResult> InvokeAsync(LoginoutArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.Loginout");

        return await mediator.Send(args, new CancellationToken());
    }
}
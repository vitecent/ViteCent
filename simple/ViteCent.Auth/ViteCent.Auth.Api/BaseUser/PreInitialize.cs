#region

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
public class PreInitialize(ILogger<PreInitialize> logger, IMediator mediator) : BaseApi<PreInitializeArgs, DataResult<PreInitializeResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("PreInitialize")]
    public override async Task<DataResult<PreInitializeResult>> InvokeAsync(PreInitializeArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseUser.PreInitialize");

        return await mediator.Send(args, new CancellationToken());
    }
}
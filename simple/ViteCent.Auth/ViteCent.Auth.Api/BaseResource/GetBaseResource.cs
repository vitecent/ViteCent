#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseResource;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseResource;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseResource")]
public class GetBaseResource(ILogger<GetBaseResource> logger, IMediator mediator) : BaseLoginApi<GetBaseResourceArgs, DataResult<BaseResourceResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseResource", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseResourceResult>> InvokeAsync(GetBaseResourceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseResource.GetBaseResource");

        if (args == null)
            return new DataResult<BaseResourceResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}
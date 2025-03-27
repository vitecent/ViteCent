#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseRole;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseRole;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseRole")]
public class GetBaseRole(ILogger<GetBaseRole> logger, IMediator mediator) : BaseLoginApi<GetBaseRoleArgs, DataResult<BaseRoleResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseRole", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<BaseRoleResult>> InvokeAsync(GetBaseRoleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseRole.GetBaseRole");

        if (args == null)
            return new DataResult<BaseRoleResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}
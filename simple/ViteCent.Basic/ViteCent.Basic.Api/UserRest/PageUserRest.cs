#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.UserRest;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("UserRest")]
public class PageUserRest(ILogger<PageUserRest> logger, IMediator mediator) : BaseLoginApi<SearchUserRestArgs, PageResult<UserRestResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "UserRest", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<UserRestResult>> InvokeAsync(SearchUserRestArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.UserRest.PageUserRest");

        if (args == null)
            return new PageResult<UserRestResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
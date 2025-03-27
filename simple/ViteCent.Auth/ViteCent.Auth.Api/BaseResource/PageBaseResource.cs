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
public class PageBaseResource(ILogger<PageBaseResource> logger, IMediator mediator) : BaseLoginApi<SearchBaseResourceArgs, PageResult<BaseResourceResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseResource", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseResourceResult>> InvokeAsync(SearchBaseResourceArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseResource.PageBaseResource");

        if (args == null)
            return new PageResult<BaseResourceResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
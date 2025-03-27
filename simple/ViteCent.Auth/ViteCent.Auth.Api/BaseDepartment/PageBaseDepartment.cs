#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseDepartment;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseDepartment")]
public class PageBaseDepartment(ILogger<PageBaseDepartment> logger, IMediator mediator) : BaseLoginApi<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseDepartment", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseDepartmentResult>> InvokeAsync(SearchBaseDepartmentArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseDepartment.PageBaseDepartment");

        if (args == null)
            return new PageResult<BaseDepartmentResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
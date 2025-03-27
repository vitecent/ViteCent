#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Auth.Api.BaseOperation;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("BaseOperation")]
public class PageBaseOperation(ILogger<PageBaseOperation> logger, IMediator mediator) : BaseLoginApi<SearchBaseOperationArgs, PageResult<BaseOperationResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Auth", "BaseOperation", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<BaseOperationResult>> InvokeAsync(SearchBaseOperationArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Api.BaseOperation.PageBaseOperation");

        if (args == null)
            return new PageResult<BaseOperationResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
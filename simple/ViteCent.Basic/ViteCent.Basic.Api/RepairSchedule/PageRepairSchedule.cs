#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.RepairSchedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("RepairSchedule")]
public class PageRepairSchedule(ILogger<PageRepairSchedule> logger, IMediator mediator) : BaseLoginApi<SearchRepairScheduleArgs, PageResult<RepairScheduleResult>>
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "RepairSchedule", "Get" })]
    [Route("Page")]
    public override async Task<PageResult<RepairScheduleResult>> InvokeAsync(SearchRepairScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.RepairSchedule.PageRepairSchedule");

        if (args == null)
            return new PageResult<RepairScheduleResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
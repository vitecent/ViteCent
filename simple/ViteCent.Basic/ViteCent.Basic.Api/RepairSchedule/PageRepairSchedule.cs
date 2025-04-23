/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

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
/// 补卡申请分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("RepairSchedule")]
public class PageRepairSchedule(
    ILogger<PageRepairSchedule> logger,
    IMediator mediator)
    : BaseLoginApi<SearchRepairScheduleArgs, PageResult<RepairScheduleResult>>
{
    /// <summary>
    /// 补卡申请分页
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
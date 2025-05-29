#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Basic.Api.Schedule;

/// <summary>
/// 排班信息分页接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class ListSchedule(
    ILogger<ListSchedule> logger,
    IMediator mediator) : BaseApi<ListScheduleArgs, PageResult<UserScheduleResult>>
{
    /// <summary>
    /// 排班信息分页
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    [Route("List")]
    public override async Task<PageResult<UserScheduleResult>> InvokeAsync(ListScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.ListSchedule");

        if (args is null)
            return new PageResult<UserScheduleResult>(500, "参数不能为空");

        return await mediator.Send(args);
    }
}
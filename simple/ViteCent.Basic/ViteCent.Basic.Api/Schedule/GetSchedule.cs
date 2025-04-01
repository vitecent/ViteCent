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
/// 获取排班信息接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class GetSchedule(ILogger<GetSchedule> logger, IMediator mediator) : BaseLoginApi<GetScheduleArgs, DataResult<ScheduleResult>>
{
    /// <summary>
    /// 获取排班信息
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Basic", "Schedule", "Get" })]
    [Route("Get")]
    public override async Task<DataResult<ScheduleResult>> InvokeAsync(GetScheduleArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Api.Schedule.GetSchedule");

        if (args == null)
            return new DataResult<ScheduleResult>(500, "参数不能为空");

        return await mediator.Send(args, new CancellationToken());
    }
}
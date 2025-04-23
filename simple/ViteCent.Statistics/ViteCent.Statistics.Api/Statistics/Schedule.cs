#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using ViteCent.Core.Data;
using ViteCent.Core.Web.Api;
using ViteCent.Core.Web.Filter;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Api.Statistics;

/// <summary>
/// 考勤统计接口
/// </summary>
/// <param name="logger"></param>
/// <param name="mediator"></param>
[ApiController]
[ServiceFilter(typeof(BaseLoginFilter))]
[Route("Schedule")]
public class Schedule(
    ILogger<Schedule> logger,
    IMediator mediator) : BaseLoginApi<StatisticsScheduleStatisticsArgs, DataResult<ScheduleStatisticsResult>>
{
    /// <summary>
    /// 考勤统计
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [HttpPost]
    [TypeFilter(typeof(BaseAuthFilter), Arguments = new object[] { "Statistics", "Schedule", "Statistics" })]
    [Route("Page")]
    public override async Task<DataResult<ScheduleStatisticsResult>> InvokeAsync(StatisticsScheduleStatisticsArgs args)
    {
        logger.LogInformation("Invoke ViteCent.Statistics.Api.Statistics.Schedule");

        var cancellationToken = new CancellationToken();
        var validator = new StatisticsScheduleStatisticsValidator();

        var check = await validator.ValidateAsync(args, cancellationToken);

        if (!check.IsValid)
            return new DataResult<ScheduleStatisticsResult>(500,
                check.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty);

        return await mediator.Send(args);
    }
}
#region

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ViteCent.Core.Data;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Application.Statistics;

/// <summary>
/// 考勤统计仓储
/// </summary>
public class Schedule(
    ILogger<Schedule> logger,
    IMapper mapper,
    IMediator mediator,
    IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<StatisticsScheduleStatisticsArgs, DataResult<ScheduleStatisticsResult>>
{
    /// <summary>
    /// 用户信息
    /// </summary>
    private BaseUserInfo user = new();

    /// <summary>
    /// 考勤统计
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DataResult<ScheduleStatisticsResult>> Handle(StatisticsScheduleStatisticsArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Statistics.Application.Statistics.Schedule");

        user = httpContextAccessor.InitUser();

        var now = DateTime.Now;

        var args = mapper.Map<StatisticsScheduleStatisticsEntityArgs>(request);

        args.CompanyId = user.Company.Id;
        args.DepartmentId = user.Department.Id;
        args.PositionId = user.Position.Id;

        if (request.Type == StatisticsScheduleTypeEnum.Year)
        {
            args.StartTime = DateTime.Parse($"{request.Date}-01-01 00:00:00");
            args.EndTime = args.StartTime.AddYears(1).AddSeconds(-1);
        }
        else
        {
            args.StartTime = DateTime.Parse($"{request.Date}-01 00:00:00");
            args.EndTime = args.StartTime.AddMonths(1).AddSeconds(-1);
        }

        return await mediator.Send(args, cancellationToken);
    }
}
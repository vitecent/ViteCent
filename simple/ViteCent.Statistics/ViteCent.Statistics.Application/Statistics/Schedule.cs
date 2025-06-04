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
/// <remarks>
/// 该类负责处理考勤统计相关的业务逻辑，包括：
/// 1. 根据请求参数计算统计时间范围（按年或按月）
/// 2. 自动获取当前用户的公司、部门和职位信息
/// 3. 调用底层服务执行实际的统计计算
/// </remarks>
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
    /// 执行考勤统计计算
    /// </summary>
    /// <param name="request">统计请求参数，包含统计类型（年/月）和日期信息</param>
    /// <param name="cancellationToken">取消令牌，用于支持操作的取消</param>
    /// <returns>返回包含统计结果的数据结果对象</returns>
    /// <remarks>
    /// 处理流程：
    /// 1. 记录调用日志
    /// 2. 初始化当前用户信息
    /// 3. 根据统计类型计算时间范围
    /// 4. 映射并补充统计参数
    /// 5. 调用统计服务执行计算
    /// </remarks>
    public async Task<DataResult<ScheduleStatisticsResult>> Handle(StatisticsScheduleStatisticsArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Statistics.Application.Statistics.Schedule");

        user = httpContextAccessor.InitUser();

        var now = DateTime.Now;

        var args = mapper.Map<StatisticsScheduleStatisticsEntityArgs>(request);

        args.CompanyId = user?.Company?.Id ?? string.Empty;
        args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
        args.PositionId = user?.Position?.Id ?? string.Empty;

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
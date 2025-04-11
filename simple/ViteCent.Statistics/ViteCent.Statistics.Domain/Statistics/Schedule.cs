#region

using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Orm.SqlSugar;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Domain.Statistics
{
    /// <summary>
    /// 考勤统计
    /// </summary>
    /// <param name="logger"></param>
    public class Schedule(ILogger<Schedule> logger) : IRequestHandler<StatisticsScheduleStatisticsEntityArgs, DataResult<ScheduleStatisticsResult>>
    {
        /// <summary>
        /// 考勤统计
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DataResult<ScheduleStatisticsResult>> Handle(StatisticsScheduleStatisticsEntityArgs request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Invoke ViteCent.Statistics.Domain.Statistics.Schedule");

            var result = new DataResult<ScheduleStatisticsResult>()
            {
                Data = new()
            };

            var authClient = new SqlSugarFactory("ViteCent.Auth");

            var userQuery = authClient.Query<BaseUserEntity>().Where(x => x.Status == (int)StatusEnum.Enable && x.IsSuper != (int)YesNoEnum.Yes);

            if (!string.IsNullOrWhiteSpace(request.CompanyId))
                userQuery = userQuery.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrWhiteSpace(request.DepartmentId))
                userQuery = userQuery.Where(x => x.DepartmentId == request.DepartmentId);

            if (!string.IsNullOrWhiteSpace(request.PositionId))
                userQuery = userQuery.Where(x => x.PositionId == request.PositionId);

            if (!string.IsNullOrWhiteSpace(request.Query))
                userQuery = userQuery.Where(x => x.RealName.Contains(request.Query));

            var items = await userQuery.Select(x => new StatisticsScheduleStatisticsResultItem()
            {
                Name = x.RealName,
                Values = new()
            }).ToListAsync(cancellationToken);

            if (items.Count == 0)
                return result;

            result.Data.Items = items;

            var basicClient = new SqlSugarFactory("ViteCent.Basic");

            var jobQuery = basicClient.Query<ScheduleTypeEntity>().Where(x => x.ScheduleType == (int)ScheduleTypeEnum.Schedule);

            if (!string.IsNullOrWhiteSpace(request.CompanyId))
                jobQuery = jobQuery.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrWhiteSpace(request.DepartmentId))
                jobQuery = jobQuery.Where(x => x.DepartmentId == request.DepartmentId);

            jobQuery = jobQuery.OrderByDescending(x => x.CreateTime);

            var scheduleTypes = await jobQuery.Select(x => new ScheduleTypeResult()
            {
                Name = x.Name
            }).ToListAsync(cancellationToken);

            if (scheduleTypes.Count == 0)
                return result;

            var jobs = scheduleTypes.Select(x => x.Name).ToList();

            result.Data.Jobs.AddRange(jobs);

            var query = basicClient.Query<ScheduleEntity>().Where(x => jobs.Contains(x.Shift));

            if (!string.IsNullOrWhiteSpace(request.CompanyId))
                query = query.Where(x => x.CompanyId == request.CompanyId);

            if (!string.IsNullOrWhiteSpace(request.DepartmentId))
                query = query.Where(x => x.DepartmentId == request.DepartmentId);

            query.Where(x => (x.StartTime >= request.StartTime && x.StartTime <= request.EndTime) ||
                   (x.EndTime >= request.StartTime && x.EndTime <= request.EndTime) ||
                   (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime));

            var list = await query.Select(x => new ScheduleResult
            {
                Job = x.Shift,
                UserName = x.UserName,
                FirstTime = x.FirstTime,
                LastTime = x.LastTime
            }).ToListAsync(cancellationToken);

            if (list.Count == 0)
                return result;

            foreach (var item in result.Data.Items)
                foreach (var job in result.Data.Jobs)
                {
                    var datas = list.Where(x => x.UserName == item.Name && x.Job == job).ToList();
                    if (datas.Count == 0)
                        item.Values.Add([0, 0]);
                    else
                    {
                        var data = GetData(datas);

                        item.Values.Add(data);
                    }
                }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        private static List<double> GetData(List<ScheduleResult> datas)
        {
            var result = new List<double>
            {
                datas.Count
            };

            var diff = 0D;

            foreach (var item in datas)
            {
                if (item.FirstTime.HasValue && item.LastTime.HasValue && item.LastTime >= item.FirstTime)
                    diff += (item.LastTime.Value - item.FirstTime.Value).TotalHours;
            }

            diff = Math.Round(diff, 2, MidpointRounding.AwayFromZero);

            result.Add(diff);

            return result;
        }
    }
}
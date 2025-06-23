#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Basic.Data.BasePost;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Orm.SqlSugar;
using ViteCent.Statistics.Data.Statistics;

#endregion

namespace ViteCent.Statistics.Domain.Statistics;

public class Schedule(ILogger<Schedule> logger)
    : IRequestHandler<StatisticsScheduleStatisticsEntityArgs, DataResult<ScheduleStatisticsResult>>
{
    public async Task<DataResult<ScheduleStatisticsResult>> Handle(StatisticsScheduleStatisticsEntityArgs request,
CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Statistics.Domain.Statistics.Schedule");

        var result = new DataResult<ScheduleStatisticsResult>
        {
            Data = new ScheduleStatisticsResult()
        };

        var authClient = new SqlSugarFactory("ViteCent.Auth");

        var userQuery = authClient.Query<BaseUserEntity>()
    .Where(x => x.Status == (int)StatusEnum.Enable && x.IsSuper != (int)YesNoEnum.Yes);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            userQuery = userQuery.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            userQuery = userQuery.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.PositionId))
            userQuery = userQuery.Where(x => x.PositionId == request.PositionId);

        if (!string.IsNullOrWhiteSpace(request.Query))
            userQuery = userQuery.Where(x => x.RealName.Contains(request.Query));

        var users = await userQuery.Select(x => new StatisticsScheduleStatisticsResultItem
        {
            Name = x.RealName,
            Values = new List<List<double>>()
        }).ToListAsync(cancellationToken);

        if (users.Count == 0)
            return result;

        result.Data.Items = users;

        var basicClient = new SqlSugarFactory("ViteCent.Basic");

        //岗位
        var postQuery = basicClient.Query<BasePostEntity>();

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            postQuery = postQuery.Where(x => x.CompanyId == request.CompanyId);

        postQuery = postQuery.OrderByDescending(x => x.CreateTime);

        var posts = await postQuery.Select(x => new BasePostResult
        {
            Name = x.Name
        }).ToListAsync(cancellationToken);

        if (posts.Count == 0)
            return result;

        var jobs = posts.Select(x => x.Name).ToList();

        result.Data.Jobs.AddRange(jobs);

        //排班
        var query = basicClient.Query<ScheduleEntity>().Where(x => jobs.Contains(x.TypeName));

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query = query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query = query.Where(x => x.DepartmentId == request.DepartmentId);

        query.Where(x => x.SceduleTimes >= request.StartTime && x.SceduleTimes <= request.EndTime);

        var list = await query.Select(x => new ScheduleResult
        {
            PostName = x.PostName,
            UserName = x.UserName,
            SignTimes = x.SignTimes,
        }).ToListAsync(cancellationToken);

        if (list.Count == 0)
            return result;

        foreach (var item in result.Data.Items)
            foreach (var job in result.Data.Jobs)
            {
                var datas = list.Where(x => x.UserName == item.Name && x.PostName == job).ToList();

                if (datas.Count == 0)
                {
                    item.Values.Add([0, 0]);
                }
                else
                {
                    var data = GetData(datas);

                    item.Values.Add(data);
                }
            }

        return result;
    }

    private static List<double> GetData(List<ScheduleResult> datas)
    {
        var now = DateTime.Now.Date;

        var result = new List<double>();

        var diff = 0D;

        foreach (var item in datas)
            try
            {
                if (!string.IsNullOrWhiteSpace(item.SignTimes))
                {
                    var arrays = item.SignTimes.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

                    foreach (var array in arrays)
                    {
                        var times = array.Split('-', StringSplitOptions.RemoveEmptyEntries).ToList();

                        if (times.Count != 2)
                            continue;

                        var start = $"{now:yyyy-MM-dd} {times[0]}:00";
                        var end = $"{now:yyyy-MM-dd} {times[1]}:59";

                        if (DateTime.TryParse(start, out var startTime) && DateTime.TryParse(end, out var endTime))
                        {
                            if (startTime > endTime)
                                endTime = endTime.AddDays(1);

                            diff += (endTime - startTime).TotalHours;
                        }
                    }
                }
            }
            catch (Exception)
            {
                continue;
            }

        diff = Math.Round(diff, 2, MidpointRounding.AwayFromZero);

        result.Add(diff);
        result.Add(datas.Count);

        return result;
    }
}
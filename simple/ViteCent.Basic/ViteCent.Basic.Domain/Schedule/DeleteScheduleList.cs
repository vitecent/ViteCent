#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Schedule;

/// <summary>
/// 批量排班信息判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class DeleteScheduleList(ILogger<DeleteScheduleList> logger)
    : BaseDomain<ScheduleEntity>, IRequestHandler<DeleteScheduleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 批量排班信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(DeleteScheduleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.DeleteSchedule");

        var query = Client.Query<ScheduleEntity>();

        request.CompanyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.CompanyId));

        request.DepartmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.DepartmentIds.Count > 0)
            query.Where(x => request.DepartmentIds.Contains(x.DepartmentId));

        request.UserIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.UserIds.Count > 0)
            query.Where(x => request.UserIds.Contains(x.UserId));

        query.Where(x => x.SceduleTimes >= request.StartTime && x.SceduleTimes <= request.EndTime);

        var entitys = await query.ToListAsync(cancellationToken);

        if (entitys.Count > 0)
            return await DeleteAsync(entitys);

        return new BaseResult();
    }
}
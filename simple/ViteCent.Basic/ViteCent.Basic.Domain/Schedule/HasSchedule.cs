#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Schedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class HasSchedule(ILogger<HasSchedule> logger) : BaseDomain<ScheduleEntity>, IRequestHandler<HasScheduleEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(HasScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.HasSchedule");

        var query = Client.Query<ScheduleEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        query.Where(x => (x.StartTime >= request.StartTime && x.StartTime <= request.EndTime) ||
            (x.EndTime >= request.StartTime && x.EndTime <= request.EndTime) ||
            (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "排班重复");

        return new BaseResult();
    }
}
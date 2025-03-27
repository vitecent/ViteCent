#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.RepairAttendance;
using ViteCent.Basic.Entity.RepairAttendance;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairAttendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class GetRepairAttendance(ILogger< GetRepairAttendance> logger) : BaseDomain<RepairAttendanceEntity>, IRequestHandler<GetRepairAttendanceEntityArgs, RepairAttendanceEntity>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<RepairAttendanceEntity> Handle(GetRepairAttendanceEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairAttendance.GetRepairAttendance");

        var query = Client.Query<RepairAttendanceEntity>();
        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        return await query.FirstAsync();
    }
}
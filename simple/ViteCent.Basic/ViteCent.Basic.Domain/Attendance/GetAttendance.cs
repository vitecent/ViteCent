#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.Attendance;
using ViteCent.Basic.Entity.Attendance;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Attendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class GetAttendance(ILogger< GetAttendance> logger) : BaseDomain<AttendanceEntity>, IRequestHandler<GetAttendanceEntityArgs, AttendanceEntity>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AttendanceEntity> Handle(GetAttendanceEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Attendance.GetAttendance");

        var query = Client.Query<AttendanceEntity>();
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
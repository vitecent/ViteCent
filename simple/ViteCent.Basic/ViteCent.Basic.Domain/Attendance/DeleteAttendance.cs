#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.Attendance;
using ViteCent.Basic.Entity.Attendance;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Attendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class DeleteAttendance(ILogger<DeleteAttendance> logger) : BaseDomain<AttendanceEntity>, IRequestHandler<DeleteAttendanceEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteAttendanceEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Attendance.DeleteAttendance");

         var query = Client.Query<AttendanceEntity>();
        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        var entity = await query.FirstAsync();

        return await base.DeleteAsync(entity);
    }
}
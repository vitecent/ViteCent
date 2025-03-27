#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.Attendance;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Attendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class AddAttendance(ILogger<AddAttendance> logger) : BaseDomain<AttendanceEntity>, IRequestHandler<AddAttendanceEntity, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddAttendanceEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Attendance.AddAttendance");

        return await base.AddAsync(request);
    }
}
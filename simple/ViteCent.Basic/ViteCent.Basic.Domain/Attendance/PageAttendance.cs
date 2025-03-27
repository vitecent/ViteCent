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
public class PageAttendance(ILogger<PageAttendance> logger) : BaseDomain<AttendanceEntity>, IRequestHandler<SearchAttendanceEntityArgs, List<AttendanceEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<AttendanceEntity>> Handle(SearchAttendanceEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Attendance.PageAttendance");

        return await base.PageAsync(request);
    }
}
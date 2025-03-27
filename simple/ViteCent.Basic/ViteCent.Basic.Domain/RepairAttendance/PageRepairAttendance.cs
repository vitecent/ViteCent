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
public class PageRepairAttendance(ILogger<PageRepairAttendance> logger) : BaseDomain<RepairAttendanceEntity>, IRequestHandler<SearchRepairAttendanceEntityArgs, List<RepairAttendanceEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<RepairAttendanceEntity>> Handle(SearchRepairAttendanceEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairAttendance.PageRepairAttendance");

        return await base.PageAsync(request);
    }
}
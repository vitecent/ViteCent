#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.RepairAttendance;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairAttendance;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class EditRepairAttendance(ILogger<EditRepairAttendance> logger) : BaseDomain<RepairAttendanceEntity>, IRequestHandler<RepairAttendanceEntity, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(RepairAttendanceEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairAttendance.EditRepairAttendance");

        return await base.EditAsync(request);
    }
}
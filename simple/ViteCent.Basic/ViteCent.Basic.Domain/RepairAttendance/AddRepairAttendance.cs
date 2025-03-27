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
public class AddRepairAttendance(ILogger<AddRepairAttendance> logger) : BaseDomain<RepairAttendanceEntity>, IRequestHandler<AddRepairAttendanceEntity, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddRepairAttendanceEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairAttendance.AddRepairAttendance");

        return await base.AddAsync(request);
    }
}
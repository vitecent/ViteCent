#region

using ViteCent.Basic.Data.RepairAttendance;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.RepairAttendance;

/// <summary>
/// </summary>
public partial class AddRepairAttendance
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddRepairAttendanceArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasRepairAttendanceEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
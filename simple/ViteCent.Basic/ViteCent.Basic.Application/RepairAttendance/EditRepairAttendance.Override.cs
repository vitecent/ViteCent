#region

using ViteCent.Basic.Data.RepairAttendance;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.RepairAttendance;

/// <summary>
/// </summary>
public partial class EditRepairAttendance
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditRepairAttendanceArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasRepairAttendanceEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
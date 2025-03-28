#region

using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// </summary>
public partial class AddShiftSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasShiftScheduleEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
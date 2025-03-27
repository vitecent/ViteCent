#region

using ViteCent.Basic.Data.Attendance;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Attendance;

/// <summary>
/// </summary>
public partial class EditAttendance
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditAttendanceArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasAttendanceEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
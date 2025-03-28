#region

using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.UserLeave;

/// <summary>
/// </summary>
public partial class AddUserLeave
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddUserLeaveArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasUserLeaveEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
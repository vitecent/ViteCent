#region

using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// </summary>
public partial class AddScheduleType
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddScheduleTypeArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasScheduleTypeEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
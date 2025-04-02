/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.RepairSchedule;

/// <summary>
/// </summary>
public partial class AddRepairSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddRepairScheduleArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasRepairScheduleEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
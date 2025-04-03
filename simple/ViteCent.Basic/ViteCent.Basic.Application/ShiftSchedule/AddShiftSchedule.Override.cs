/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// </summary>
public partial class AddShiftSchedule
{
    internal static async Task<BaseResult> OverrideHandle(AddShiftScheduleListArgs request, BaseUserInfo user)
    {
        return await Task.FromResult(new BaseResult("ok"));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasShiftScheduleEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            ScheduleId = request.ScheduleId,
            ShiftDepartmentId = request.ShiftDepartmentId,
            ShiftUserId = request.ShiftUserId,
            ShiftScheduleId = request.ShiftScheduleId
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
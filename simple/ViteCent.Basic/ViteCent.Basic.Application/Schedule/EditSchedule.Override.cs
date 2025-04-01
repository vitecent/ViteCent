#region

using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// </summary>
public partial class EditSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(ScheduleEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditScheduleArgs request, CancellationToken cancellationToken)
    {
        var args = new HasUserLeaveEntityArgs()
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = UserLeaveEnum.Pass
        };

        var data = await mediator.Send(args, cancellationToken);

        if (data.Success)
            return new BaseResult(500, "”√ªß“—«ÎºŸ");

        var hasArgs = new HasScheduleEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
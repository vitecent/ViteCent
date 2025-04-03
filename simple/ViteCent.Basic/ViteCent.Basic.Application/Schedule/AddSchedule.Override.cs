/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// </summary>
public partial class AddSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(AddScheduleListArgs request, BaseUserInfo user)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddScheduleArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty); ;

        if (hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId, user?.Token ?? string.Empty);

        if (hasDepartment.Success)
            return hasDepartment;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, request.UserId, user?.Token ?? string.Empty);

        if (hasUser.Success)
            return hasUser;

        var hasLeaveArgs = new HasUserLeaveEntityArgs()
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = UserLeaveEnum.Pass
        };

        var hasLeave = await mediator.Send(hasLeaveArgs, cancellationToken);

        if (hasLeave.Success)
            return new BaseResult(500, "用户已请假");

        var hasRestArgs = new HasUserRestEntityArgs()
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Status = UserRestEnum.Pass
        };

        var hasRest = await mediator.Send(hasRestArgs, cancellationToken);

        if (hasRest.Success)
            return new BaseResult(500, "用户已调休");

        var hasArgs = new HasScheduleEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
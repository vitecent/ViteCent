/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// </summary>
public partial class EditShiftSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(ShiftScheduleEntity entity, CancellationToken cancellationToken)
    {
        if (entity.Status != (int)ShiftScheduleEnum.Apply)
            return new BaseResult(500, "只能修改申请中的数据");

        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditShiftScheduleArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (!hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany.Data.Name;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId, user?.Token ?? string.Empty);

        if (!hasDepartment.Success)
            return hasDepartment;

        request.DepartmentName = hasDepartment.Data.Name;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, request.UserId, user?.Token ?? string.Empty);

        if (!hasUser.Success)
            return hasUser;

        request.UserName = hasUser.Data.RealName;

        var args = mapper.Map<GetScheduleEntityArgs>(request);
        args.Id = request.ScheduleId;

        var entity = await mediator.Send(args, cancellationToken);

        if (entity == null)
            return new BaseResult(500, "排班信息不存在");

        if (entity.Status != (int)ScheduleEnum.None)
            return new BaseResult(500, "只有未打卡的班次才能换班");

        request.ScheduleName = entity.Shift;

        var hasArgs = new HasShiftScheduleEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
            ScheduleId = request.ScheduleId,
            ShiftDepartmentId = request.ShiftDepartmentId,
            ShiftUserId = request.ShiftUserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
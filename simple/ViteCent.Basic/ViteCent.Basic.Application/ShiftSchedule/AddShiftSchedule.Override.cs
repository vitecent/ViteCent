/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Data.BaseDepartment;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;
using ViteCent.Core.Web;

#endregion

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// </summary>
public partial class AddShiftSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <param name="companyInvoke"></param>
    /// <param name="departmentInvoke"></param>
    /// <param name="userInvoke"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(MediatR.IMediator mediator, AddShiftScheduleListArgs request, BaseUserInfo user,
        IBaseInvoke<SearchBaseCompanyArgs, PageResult<BaseCompanyResult>> companyInvoke,
        IBaseInvoke<SearchBaseDepartmentArgs, PageResult<BaseDepartmentResult>> departmentInvoke,
        IBaseInvoke<SearchBaseUserArgs, PageResult<BaseUserResult>> userInvoke, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;
        var departmentId = user?.Department?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

            if (string.IsNullOrWhiteSpace(item.DepartmentId))
                item.DepartmentId = departmentId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var departmentIds = request.Items.Select(x => x.DepartmentId).Distinct().ToList();
        var userIds = request.Items.Select(x => x.UserId).Distinct().ToList();

        var companys = await companyInvoke.CheckCompany(companyIds, user?.Token ?? string.Empty);

        if (!companys.Success)
            return companys;

        foreach (var item in companys.Rows)
        {
            var items = request.Items.Where(x => x.CompanyId == item.Id).ToList();

            foreach (var data in items)
                data.CompanyName = item.Name;
        }

        var departments = await departmentInvoke.CheckDepartment(companyIds, departmentIds, user?.Token ?? string.Empty);

        if (!departments.Success)
            return departments;

        foreach (var item in departments.Rows)
        {
            var items = request.Items.Where(x => x.DepartmentId == item.Id).ToList();

            foreach (var data in items)
                data.DepartmentName = item.Name;
        }

        var users = await userInvoke.CheckUser(companyIds, departmentIds, userIds, user?.Token ?? string.Empty);

        if (!users.Success)
            return users;

        foreach (var item in users.Rows)
        {
            var items = request.Items.Where(x => x.UserId == item.Id).ToList();

            foreach (var data in items)
                data.UserName = item.RealName;
        }

        var scheduleIds = request.Items.Select(x => x.ScheduleId).Distinct().ToList();

        var searchArgs = new SearchScheduleArgs
        {
            Offset = 1,
            Limit = int.MaxValue,
        };

        if (companyIds.Count > 0)
            searchArgs.AddArgs("CompanyId", companyIds.ToJson(), SearchEnum.In);

        if (departmentIds.Count > 0)
            searchArgs.AddArgs("DepartmentId", departmentIds.ToJson(), SearchEnum.In);

        if (userIds.Count > 0)
            searchArgs.AddArgs("UserId", userIds.ToJson(), SearchEnum.In);

        if (scheduleIds.Count > 0)
            searchArgs.AddArgs("Id", scheduleIds.ToJson(), SearchEnum.In);

        var entitys = mediator.Send(searchArgs, cancellationToken).Result;

        if (!entitys.Success)
            return entitys;

        if (entitys.Total != scheduleIds.Count)
            return new BaseResult(500, "排班信息不存在");

        var entity = entitys.Rows.FirstOrDefault(x => x.Status != (int)ScheduleEnum.None);

        if (entity != null)
            return new BaseResult(500, "只有未打卡的班次才能换班");

        foreach (var item in entitys.Rows)
        {
            var items = request.Items.Where(x => x.ScheduleId == item.Id).ToList();

            foreach (var data in items)
                data.ScheduleName = item.Shift;
        }

        var hasListArgs = new HasShiftScheduleEntityListArgs
        {
            CompanyIds = companyIds,
            DepartmentIds = departmentIds,
            UserIds = userIds,
            ScheduleIds = scheduleIds,
            ShiftDepartmentIds = [.. request.Items.Select(x => x.ShiftDepartmentId).Distinct()],
            ShiftUserIds = [.. request.Items.Select(x => x.ShiftUserId).Distinct()],
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="topic"></param>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, ShiftScheduleEntity entity, CancellationToken cancellationToken)
    {
        if (topic != TopicEnum.Add && entity.Status != (int)ShiftScheduleEnum.Pass)
            return;

        var topicArgs = new ShiftScheduleTopicArgs
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            DepartmentId = entity.DepartmentId,
            UserId = entity.UserId,
            ScheduleId = entity.ScheduleId,
            ShiftDepartmentId = entity.ShiftDepartmentId,
            ShiftDepartmentName = entity.ShiftDepartmentName,
            ShiftUserId = entity.ShiftUserId,
            ShiftUserName = entity.ShiftUserName,
            ShiftJob = entity.Job,
        };

        await mediator.Publish(topicArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddShiftScheduleArgs request, CancellationToken cancellationToken)
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

        if (string.IsNullOrWhiteSpace(request.ShiftDepartmentId))
            request.ShiftDepartmentId = departmentId;

        var hasShiftDepartment = await departmentInvoke.CheckDepartment(request.CompanyId, request.ShiftDepartmentId, user?.Token ?? string.Empty);

        if (!hasShiftDepartment.Success)
            return hasShiftDepartment;

        request.ShiftDepartmentName = hasShiftDepartment.Data.Name;

        var hasShiftUser = await userInvoke.CheckUser(request.CompanyId, request.ShiftDepartmentId, request.ShiftUserId, user?.Token ?? string.Empty);

        if (!hasShiftUser.Success)
            return hasShiftUser;

        request.ShiftUserName = hasShiftUser.Data.RealName;

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
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// </summary>
public partial class AddBaseUserRole
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseUserRoleListArgs request, BaseUserInfo user, CancellationToken cancellationToken)
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
        var roleIds = request.Items.Select(x => x.RoleId).Distinct().ToList();

        var companys = await mediator.CheckCompany(companyIds);

        if (!companys.Success)
            return companys;

        var departments = await mediator.CheckDepartment(companyIds, departmentIds);

        if (!departments.Success)
            return departments;

        var users = await mediator.CheckUser(companyIds, departmentIds, userIds);

        if (!users.Success)
            return users;

        var roles = await mediator.CheckRole(companyIds, roleIds);

        if (!roles.Success)
            return roles;

        var hasListArgs = new HasBaseUserRoleEntityListArgs
        {
            CompanyIds = [.. request.Items.Select(x => x.CompanyId).Distinct()],
            DepartmentIds = [.. request.Items.Select(x => x.DepartmentId).Distinct()],
            RoleIds = [.. request.Items.Select(x => x.RoleId).Distinct()],
            UserIds = [.. request.Items.Select(x => x.UserId).Distinct()],
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseUserRoleArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await mediator.CheckDepartment(request.CompanyId, request.DepartmentId);

        if (hasDepartment.Success)
            return hasDepartment;

        var hasUser = await mediator.CheckUser(request.CompanyId, request.DepartmentId, request.UserId);

        if (hasUser.Success)
            return hasUser;

        var hasRole = await mediator.CheckRole(request.CompanyId, request.RoleId);

        if (hasRole.Success)
            return hasRole;

        var hasArgs = new HasBaseUserRoleEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            RoleId = request.RoleId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
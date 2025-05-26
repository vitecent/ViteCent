/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.UserRest;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.UserRest;

/// <summary>
/// </summary>
public partial class EditUserRest
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(UserRestEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditUserRestArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (!hasCompany.Success)
            return hasCompany;
        else request.CompanyName = hasCompany?.Data?.Name ?? string.Empty;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment =
            await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId,
                user?.Token ?? string.Empty);

        if (!hasDepartment.Success)
            return hasDepartment;
        else request.DepartmentName = hasDepartment?.Data?.Name ?? string.Empty;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, string.Empty, request.UserId,
            user?.Token ?? string.Empty);

        if (!hasUser.Success)
            return hasUser;
        else request.UserName = hasUser?.Data?.RealName ?? string.Empty;

        var hasArgs = new HasUserRestEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
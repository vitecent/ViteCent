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
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(UserRestEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(EditUserRestArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await companyInvoke.CheckCompany(request.CompanyId, user?.Token ?? string.Empty);

        if (!hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany?.Data?.Name;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment =
            await departmentInvoke.CheckDepartment(request.CompanyId, request.DepartmentId,
                user?.Token ?? string.Empty);

        if (!hasDepartment.Success)
            return hasDepartment;

        request.DepartmentName = hasDepartment?.Data?.Name;

        var hasUser = await userInvoke.CheckUser(request.CompanyId, request.DepartmentId, string.Empty, request.UserId,
            user?.Token ?? string.Empty);

        if (!hasUser.Success)
            return hasUser;

        request.UserName = hasUser?.Data?.RealName;

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
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
public partial class AddBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(AddBaseUserListArgs request, BaseUserInfo user)
    {
        return await Task.FromResult(new BaseResult("ok"));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseUserArgs request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Username) && !string.IsNullOrWhiteSpace(request.Password))
            request.Password = $"{request.Username}{request.Password}{Const.Salf}".EncryptMD5();

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

        var positionId = user?.Position?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.PositionId))
            request.PositionId = positionId;

        var hasPosition = await mediator.CheckPosition(request.CompanyId, request.PositionId);

        if (hasPosition.Success)
            return hasPosition;

        var hasArgs = new HasBaseUserEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserNo = request.UserNo,
            Username = request.Username,
            RealName = request.RealName,
            IdCard = request.IdCard,
            Email = request.Email,
            Phone = request.Phone
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
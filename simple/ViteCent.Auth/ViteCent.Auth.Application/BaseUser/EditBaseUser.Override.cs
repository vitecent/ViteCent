/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// </summary>
public partial class EditBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseUserEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseUserArgs request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrWhiteSpace(request.Username) && !string.IsNullOrWhiteSpace(request.Password))
            request.Password = $"{request.Username}{request.Password}{BaseConst.Salf}".EncryptMD5();

        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        request.CompanyName = hasCompany.Data.Name;

        var departmentId = user?.Department?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.DepartmentId))
            request.DepartmentId = departmentId;

        var hasDepartment = await mediator.CheckDepartment(request.CompanyId, request.DepartmentId);

        if (hasDepartment.Success)
            return hasDepartment;

        request.DepartmentName = hasDepartment.Data.Name;

        var positionId = user?.Position?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.PositionId))
            request.PositionId = positionId;

        var hasPosition = await mediator.CheckPosition(request.CompanyId, request.PositionId);

        if (hasPosition.Success)
            return hasPosition;

        request.PositionName = hasPosition.Data.Name;

        var hasArgs = new HasBaseUserEntityArgs
        {
            Id = request.Id,
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
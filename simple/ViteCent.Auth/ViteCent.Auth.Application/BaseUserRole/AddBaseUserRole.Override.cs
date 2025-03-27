#region

using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// </summary>
public partial class AddBaseUserRole
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddBaseUserRoleArgs request, CancellationToken cancellationToken)
    {
        request.Status = (int)StatusEnum.Enable;

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
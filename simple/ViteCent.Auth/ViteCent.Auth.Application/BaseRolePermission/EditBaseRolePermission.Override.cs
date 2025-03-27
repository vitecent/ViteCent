#region

using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// </summary>
public partial class EditBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(EditBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseRolePermissionEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            RoleId = request.RoleId,
            SystemId = request.SystemId,
            ResourceId = request.ResourceId,
            OperationId = request.OperationId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
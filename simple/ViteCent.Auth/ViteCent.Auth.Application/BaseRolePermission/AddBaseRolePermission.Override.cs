#region

using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// </summary>
public partial class AddBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> OverrideHandle(AddBaseRolePermissionArgs request, CancellationToken cancellationToken)
    {
        request.Status = (int)StatusEnum.Enable;

        var hasArgs = new HasBaseRolePermissionEntityArgs
        {
            CompanyId = request.CompanyId,
            RoleId = request.RoleId,
            SystemId = request.SystemId,
            ResourceId = request.ResourceId,
            OperationId = request.OperationId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// </summary>
public partial class EditBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseRolePermissionEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseRolePermissionArgs request, CancellationToken cancellationToken)
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
/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRolePermission;

/// <summary>
/// 删除角色权限领域
/// </summary>
/// <param name="logger"></param>
public class DeleteBaseRolePermission(
    ILogger<DeleteBaseRolePermission> logger)
    : BaseDomain<DeleteBaseRolePermissionEntity>, IRequestHandler<DeleteBaseRolePermissionEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除角色权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseRolePermissionEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.DeleteBaseRolePermission");

        return await DeleteAsync(request);
    }
}
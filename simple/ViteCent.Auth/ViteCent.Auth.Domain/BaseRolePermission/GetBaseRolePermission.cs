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
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRolePermission;

/// <summary>
/// 获取角色权限
/// </summary>
/// <param name="logger"></param>
public class GetBaseRolePermission(ILogger<GetBaseRolePermission> logger) : BaseDomain<BaseRolePermissionEntity>, IRequestHandler<GetBaseRolePermissionEntityArgs, BaseRolePermissionEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseRolePermissionEntity> Handle(GetBaseRolePermissionEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.GetBaseRolePermission");

        var query = Client.Query<BaseRolePermissionEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.RoleId))
            query.Where(x => x.RoleId == request.RoleId);

        if (!string.IsNullOrWhiteSpace(request.SystemId))
            query.Where(x => x.SystemId == request.SystemId);

        if (!string.IsNullOrWhiteSpace(request.ResourceId))
            query.Where(x => x.ResourceId == request.ResourceId);

        if (!string.IsNullOrWhiteSpace(request.OperationId))
            query.Where(x => x.OperationId == request.OperationId);

        return await query.FirstAsync(cancellationToken);
    }
}
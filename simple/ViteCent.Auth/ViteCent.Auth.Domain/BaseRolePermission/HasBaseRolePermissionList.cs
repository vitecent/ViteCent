/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
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
/// 批量角色权限判重
/// </summary>
/// <param name="logger"></param>
public class HasBaseRolePermissionList(ILogger<HasBaseRolePermissionList> logger)
    : BaseDomain<BaseRolePermissionEntity>, IRequestHandler<HasBaseRolePermissionEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量角色权限判重
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(HasBaseRolePermissionEntityListArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.HasBaseRolePermission");

        var query = Client.Query<BaseRolePermissionEntity>();

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

        if (request.RoleIds.Count > 0)
            query.Where(x => request.RoleIds.Contains(x.RoleId));

        if (request.SystemIds.Count > 0)
            query.Where(x => request.SystemIds.Contains(x.SystemId));

        if (request.ResourceIds.Count > 0)
            query.Where(x => request.ResourceIds.Contains(x.ResourceId));

        if (request.OperationIds.Count > 0)
            query.Where(x => request.OperationIds.Contains(x.OperationId));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}
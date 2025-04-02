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
/// 角色权限分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseRolePermission(ILogger<PageBaseRolePermission> logger) : BaseDomain<BaseRolePermissionEntity>, IRequestHandler<SearchBaseRolePermissionEntityArgs, List<BaseRolePermissionEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 角色权限分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseRolePermissionEntity>> Handle(SearchBaseRolePermissionEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRolePermission.PageBaseRolePermission");

        return await base.PageAsync(request);
    }
}
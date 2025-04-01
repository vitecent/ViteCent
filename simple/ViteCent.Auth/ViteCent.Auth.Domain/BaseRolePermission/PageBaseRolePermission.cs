#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRolePermission;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseRolePermission(ILogger<PageBaseRolePermission> logger) : BaseDomain<BaseRolePermissionEntity>, IRequestHandler<SearchBaseRolePermissionEntityArgs, List<BaseRolePermissionEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
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
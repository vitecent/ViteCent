#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRole;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseRole(ILogger<PageBaseRole> logger) : BaseDomain<BaseRoleEntity>, IRequestHandler<SearchBaseRoleEntityArgs, List<BaseRoleEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseRoleEntity>> Handle(SearchBaseRoleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseRole.PageBaseRole");

        return await base.PageAsync(request);
    }
}
#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseUserRole;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUserRole;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseUserRole(ILogger<PageBaseUserRole> logger) : BaseDomain<BaseUserRoleEntity>, IRequestHandler<SearchBaseUserRoleEntityArgs, List<BaseUserRoleEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseUserRoleEntity>> Handle(SearchBaseUserRoleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseUserRole.PageBaseUserRole");

        return await base.PageAsync(request);
    }
}
#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseRole;

/// <summary>
/// 角色信息分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseRole(ILogger<PageBaseRole> logger) : BaseDomain<BaseRoleEntity>, IRequestHandler<SearchBaseRoleEntityArgs, List<BaseRoleEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 角色信息分页
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
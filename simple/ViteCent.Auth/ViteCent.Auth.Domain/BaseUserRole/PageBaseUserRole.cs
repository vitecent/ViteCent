/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseUserRole;

/// <summary>
/// 用户角色分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseUserRole(ILogger<PageBaseUserRole> logger) : BaseDomain<BaseUserRoleEntity>, IRequestHandler<SearchBaseUserRoleEntityArgs, List<BaseUserRoleEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 用户角色分页
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
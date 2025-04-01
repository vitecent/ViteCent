#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDepartment;

/// <summary>
/// 部门信息分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseDepartment(ILogger<PageBaseDepartment> logger) : BaseDomain<BaseDepartmentEntity>, IRequestHandler<SearchBaseDepartmentEntityArgs, List<BaseDepartmentEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 部门信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseDepartmentEntity>> Handle(SearchBaseDepartmentEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDepartment.PageBaseDepartment");

        return await base.PageAsync(request);
    }
}
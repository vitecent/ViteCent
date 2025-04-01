#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDepartment;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDepartment;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseDepartment(ILogger<PageBaseDepartment> logger) : BaseDomain<BaseDepartmentEntity>, IRequestHandler<SearchBaseDepartmentEntityArgs, List<BaseDepartmentEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
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
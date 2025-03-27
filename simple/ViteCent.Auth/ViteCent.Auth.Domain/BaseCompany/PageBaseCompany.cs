#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseCompany;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseCompany;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseCompany(ILogger<PageBaseCompany> logger) : BaseDomain<BaseCompanyEntity>, IRequestHandler<SearchBaseCompanyEntityArgs, List<BaseCompanyEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseCompanyEntity>> Handle(SearchBaseCompanyEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseCompany.PageBaseCompany");

        return await base.PageAsync(request);
    }
}
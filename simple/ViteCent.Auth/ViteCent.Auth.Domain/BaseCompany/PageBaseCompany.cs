/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseCompany;

/// <summary>
/// 公司信息分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseCompany(ILogger<PageBaseCompany> logger) : BaseDomain<BaseCompanyEntity>, IRequestHandler<SearchBaseCompanyEntityArgs, List<BaseCompanyEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 公司信息分页
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
/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseCompany;

/// <summary>
/// 批量公司信息判重
/// </summary>
/// <param name="logger"></param>
public class HasBaseCompanyList(ILogger<HasBaseCompanyList> logger)
    : BaseDomain<BaseCompanyEntity>, IRequestHandler<HasBaseCompanyEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 批量公司信息判重
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(HasBaseCompanyEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseCompany.HasBaseCompany");

        var query = Client.Query<BaseCompanyEntity>();

        if (request.Codes.Count > 1)
            query = query.Where(x => request.Codes.Contains(x.Code));

        if (request.Names.Count > 1)
            query = query.Where(x => request.Names.Contains(x.Name));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "编码 或 名称 重复");

        return new BaseResult();
    }
}
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
/// 获取公司信息
/// </summary>
/// <param name="logger"></param>
public class GetBaseCompany(ILogger<GetBaseCompany> logger) : BaseDomain<BaseCompanyEntity>, IRequestHandler<GetBaseCompanyEntityArgs, BaseCompanyEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 获取公司信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseCompanyEntity> Handle(GetBaseCompanyEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseCompany.GetBaseCompany");

        var query = Client.Query<BaseCompanyEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        return await query.FirstAsync();
    }
}
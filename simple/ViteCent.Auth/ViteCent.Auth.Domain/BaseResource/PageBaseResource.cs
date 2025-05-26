/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseResource;

/// <summary>
/// 资源信息分页领域
/// </summary>
/// <param name="logger"></param>
public class PageBaseResource(ILogger<PageBaseResource> logger) : BaseDomain<BaseResourceEntity>, IRequestHandler<SearchBaseResourceEntityArgs, List<BaseResourceEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 资源信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseResourceEntity>> Handle(SearchBaseResourceEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseResource.PageBaseResource");

        return await base.PageAsync(request);
    }
}
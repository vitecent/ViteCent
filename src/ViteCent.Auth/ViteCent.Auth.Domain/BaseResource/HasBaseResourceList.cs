/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseResource;

/// <summary>
/// 批量资源信息判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseResourceList(ILogger<HasBaseResourceList> logger) : BaseDomain<BaseResourceEntity>,
    IRequestHandler<HasBaseResourceEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 批量资源信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseResourceEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseResource.HasBaseResource");

        var query = Client.Query<BaseResourceEntity>();

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

        if (request.SystemIds.Count > 0)
            query.Where(x => request.SystemIds.Contains(x.SystemId));

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
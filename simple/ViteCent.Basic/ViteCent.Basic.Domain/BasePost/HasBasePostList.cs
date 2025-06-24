/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.BasePost;

/// <summary>
/// 批量职位信息判重领域
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBasePostList(
    ILogger<HasBasePostList> logger)
    : BaseDomain<BasePostEntity>, IRequestHandler<HasBasePostEntityListArgs, BaseResult>
{
    /// <summary>
    /// 职位信息库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 批量职位信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBasePostEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.BasePost.HasBasePost");

        var query = Client.Query<BasePostEntity>();

        request.CompanyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.Id));

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
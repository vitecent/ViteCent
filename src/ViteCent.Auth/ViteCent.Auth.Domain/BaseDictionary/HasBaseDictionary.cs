/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BaseDictionary;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDictionary;

/// <summary>
/// 字典信息判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasBaseDictionary(ILogger<HasBaseDictionary> logger) : BaseDomain<BaseDictionaryEntity>,
    IRequestHandler<HasBaseDictionaryEntityArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Auth";

    /// <summary>
    /// 字典信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasBaseDictionaryEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDictionary.HasBaseDictionary");

        var query = Client.Query<BaseDictionaryEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.Code))
            query.Where(x => x.Code == request.Code);

        if (!string.IsNullOrWhiteSpace(request.Name))
            query.Where(x => x.Name == request.Name);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "编码 或 名称 重复");

        return new BaseResult();
    }
}
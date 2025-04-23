/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseDictionary;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDictionary;

/// <summary>
/// 字典信息分页
/// </summary>
/// <param name="logger"></param>
public class PageBaseDictionary(ILogger<PageBaseDictionary> logger) : BaseDomain<BaseDictionaryEntity>, IRequestHandler<SearchBaseDictionaryEntityArgs, List<BaseDictionaryEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 字典信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseDictionaryEntity>> Handle(SearchBaseDictionaryEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDictionary.PageBaseDictionary");

        return await base.PageAsync(request);
    }
}
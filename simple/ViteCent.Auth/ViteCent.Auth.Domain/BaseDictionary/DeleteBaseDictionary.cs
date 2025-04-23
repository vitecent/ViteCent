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
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseDictionary;

/// <summary>
/// 删除字典信息
/// </summary>
/// <param name="logger"></param>
public class DeleteBaseDictionary(
    ILogger<DeleteBaseDictionary> logger)
    : BaseDomain<DeleteBaseDictionaryEntity>, IRequestHandler<DeleteBaseDictionaryEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 删除字典信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(DeleteBaseDictionaryEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseDictionary.DeleteBaseDictionary");

        return await DeleteAsync(request);
    }
}
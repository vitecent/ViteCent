/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseOperation;

/// <summary>
/// 操作信息分页领域
/// </summary>
/// <param name="logger"></param>
public class PageBaseOperation(ILogger<PageBaseOperation> logger) : BaseDomain<BaseOperationEntity>, IRequestHandler<SearchBaseOperationEntityArgs, List<BaseOperationEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// 操作信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseOperationEntity>> Handle(SearchBaseOperationEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseOperation.PageBaseOperation");

        return await base.PageAsync(request);
    }
}
#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseOperation;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseOperation(ILogger<PageBaseOperation> logger) : BaseDomain<BaseOperationEntity>, IRequestHandler<SearchBaseOperationEntityArgs, List<BaseOperationEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseOperationEntity>> Handle(SearchBaseOperationEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseOperation.PageBaseOperation");

        return await base.PageAsync(request);
    }
}
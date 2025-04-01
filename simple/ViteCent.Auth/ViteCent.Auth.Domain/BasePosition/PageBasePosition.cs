#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Data.BasePosition;
using ViteCent.Auth.Entity.BasePosition;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BasePosition;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBasePosition(ILogger<PageBasePosition> logger) : BaseDomain<BasePositionEntity>, IRequestHandler<SearchBasePositionEntityArgs, List<BasePositionEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BasePositionEntity>> Handle(SearchBasePositionEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BasePosition.PageBasePosition");

        return await base.PageAsync(request);
    }
}
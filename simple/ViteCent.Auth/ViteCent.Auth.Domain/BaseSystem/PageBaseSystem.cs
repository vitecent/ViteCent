#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Auth.Entity.BaseSystem;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Domain.BaseSystem;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageBaseSystem(ILogger<PageBaseSystem> logger) : BaseDomain<BaseSystemEntity>, IRequestHandler<SearchBaseSystemEntityArgs, List<BaseSystemEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Auth";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<BaseSystemEntity>> Handle(SearchBaseSystemEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Auth.Domain.BaseSystem.PageBaseSystem");

        return await base.PageAsync(request);
    }
}
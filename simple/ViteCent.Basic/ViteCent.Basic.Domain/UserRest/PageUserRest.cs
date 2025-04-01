#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserRest;

/// <summary>
/// 调休申请分页
/// </summary>
/// <param name="logger"></param>
public class PageUserRest(ILogger<PageUserRest> logger) : BaseDomain<UserRestEntity>, IRequestHandler<SearchUserRestEntityArgs, List<UserRestEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 调休申请分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserRestEntity>> Handle(SearchUserRestEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserRest.PageUserRest");

        return await base.PageAsync(request);
    }
}
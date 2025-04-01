#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserLeave;

/// <summary>
/// 请假申请分页
/// </summary>
/// <param name="logger"></param>
public class PageUserLeave(ILogger<PageUserLeave> logger) : BaseDomain<UserLeaveEntity>, IRequestHandler<SearchUserLeaveEntityArgs, List<UserLeaveEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 请假申请分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<UserLeaveEntity>> Handle(SearchUserLeaveEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserLeave.PageUserLeave");

        return await base.PageAsync(request);
    }
}
#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserLeave;

/// <summary>
/// 获取请假申请
/// </summary>
/// <param name="logger"></param>
public class GetUserLeave(ILogger<GetUserLeave> logger) : BaseDomain<UserLeaveEntity>, IRequestHandler<GetUserLeaveEntityArgs, UserLeaveEntity>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 获取请假申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UserLeaveEntity> Handle(GetUserLeaveEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserLeave.GetUserLeave");

        var query = Client.Query<UserLeaveEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        return await query.FirstAsync();
    }
}
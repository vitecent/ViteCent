#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.UserLeave;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class HasUserLeave(ILogger<HasUserLeave> logger) : BaseDomain<UserLeaveEntity>, IRequestHandler<HasUserLeaveEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(HasUserLeaveEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserLeave.HasUserLeave");

        var query = Client.Query<UserLeaveEntity>();

        if (request.Status != default)
            query.Where(x => x.Status == (int)request.Status);

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id == request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        query.Where(x => ((x.StartTime <= request.StartTime && x.EndTime >= request.StartTime) ||
          (x.StartTime <= request.EndTime && x.EndTime >= request.EndTime) ||
          (x.EndTime >= request.EndTime && x.EndTime <= request.EndTime)));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "请假重复");

        return new BaseResult();
    }
}
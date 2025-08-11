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
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasUserLeave(ILogger<HasUserLeave> logger)
    : BaseDomain<UserLeaveEntity>, IRequestHandler<HasUserLeaveEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasUserLeaveEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.UserLeave.HasUserLeave");

        var query = Client.Query<UserLeaveEntity>().Where(x => x.Status != (int)UserLeaveEnum.Pass);

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        query.Where(x => (x.StartTime >= request.StartTime && x.StartTime <= request.EndTime) ||
                         (x.EndTime >= request.StartTime && x.EndTime <= request.EndTime) ||
                         (x.StartTime <= request.StartTime && x.EndTime >= request.EndTime));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "请假重复");

        return new BaseResult();
    }
}
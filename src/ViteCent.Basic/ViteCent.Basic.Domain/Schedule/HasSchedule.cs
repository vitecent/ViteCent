#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.Schedule;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Schedule;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasSchedule(ILogger<HasSchedule> logger)
    : BaseDomain<ScheduleEntity>, IRequestHandler<HasScheduleEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.HasSchedule");

        var query = Client.Query<ScheduleEntity>();

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        query.Where(x => x.SceduleTimes >= request.StartTime && x.SceduleTimes <= request.EndTime);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "排班重复");

        return new BaseResult();
    }
}
/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Schedule;

/// <summary>
/// 批量排班信息判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasScheduleList(ILogger<HasScheduleList> logger)
    : BaseDomain<ScheduleEntity>, IRequestHandler<HasScheduleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 批量排班信息判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasScheduleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.HasSchedule");

        var query = Client.Query<ScheduleEntity>();

        request.CompanyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.CompanyId));

        request.DepartmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.DepartmentIds.Count > 0)
            query.Where(x => request.DepartmentIds.Contains(x.DepartmentId));

        request.UserIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.UserIds.Count > 0)
            query.Where(x => request.UserIds.Contains(x.UserId));

        query.Where(x => x.SceduleTimes >= request.StartTime && x.SceduleTimes <= request.EndTime);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}
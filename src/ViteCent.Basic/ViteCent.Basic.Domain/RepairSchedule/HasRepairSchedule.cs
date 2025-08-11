#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairSchedule;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasRepairSchedule(ILogger<HasRepairSchedule> logger) : BaseDomain<RepairScheduleEntity>,
    IRequestHandler<HasRepairScheduleEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasRepairScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairSchedule.HasRepairSchedule");

        var query = Client.Query<RepairScheduleEntity>().Where(x => x.Status != (int)RepairScheduleEnum.Pass);

        if (!string.IsNullOrWhiteSpace(request.Id))
            query.Where(x => x.Id != request.Id);

        if (!string.IsNullOrWhiteSpace(request.CompanyId))
            query.Where(x => x.CompanyId == request.CompanyId);

        if (!string.IsNullOrWhiteSpace(request.DepartmentId))
            query.Where(x => x.DepartmentId == request.DepartmentId);

        if (!string.IsNullOrWhiteSpace(request.UserId))
            query.Where(x => x.UserId == request.UserId);

        if (!string.IsNullOrWhiteSpace(request.ScheduleId))
            query.Where(x => x.ScheduleId == request.ScheduleId);

        if (request.RepairType != default)
            query.Where(x => x.RepairType == (int)request.RepairType);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}
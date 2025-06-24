#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.ShiftSchedule;

/// <summary>
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasShiftSchedule(ILogger<HasShiftSchedule> logger)
    : BaseDomain<ShiftScheduleEntity>, IRequestHandler<HasShiftScheduleEntityArgs, BaseResult>
{
    /// <summary>
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasShiftScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ShiftSchedule.HasShiftSchedule");

        var query = Client.Query<ShiftScheduleEntity>().Where(x => x.Status != (int)ShiftScheduleEnum.Pass);

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

        if (!string.IsNullOrWhiteSpace(request.ShiftDepartmentId))
            query.Where(x => x.ShiftDepartmentId == request.ShiftDepartmentId);

        if (!string.IsNullOrWhiteSpace(request.ShiftUserId))
            query.Where(x => x.ShiftUserId == request.ShiftUserId);

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "换班重复");

        return new BaseResult();
    }
}
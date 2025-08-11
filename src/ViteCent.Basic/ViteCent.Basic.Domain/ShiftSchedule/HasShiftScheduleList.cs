/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

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
/// 批量换班申请判重
/// </summary>
/// <param name="logger">日志记录器，用于记录处理器的操作日志</param>
public class HasShiftScheduleList(ILogger<HasShiftScheduleList> logger) : BaseDomain<ShiftScheduleEntity>,
    IRequestHandler<HasShiftScheduleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string Database => "ViteCent.Basic";

    /// <summary>
    /// 批量换班申请判重
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    public async Task<BaseResult> Handle(HasShiftScheduleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ShiftSchedule.HasShiftSchedule");

        var query = Client.Query<ShiftScheduleEntity>().Where(x => x.Status != (int)ShiftScheduleEnum.Pass);

        request.CompanyIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.CompanyIds.Count > 0)
            query.Where(x => request.CompanyIds.Contains(x.CompanyId));

        request.DepartmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.DepartmentIds.Count > 0)
            query.Where(x => request.DepartmentIds.Contains(x.DepartmentId));

        request.UserIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.UserIds.Count > 0)
            query.Where(x => request.UserIds.Contains(x.UserId));

        request.ScheduleIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.ScheduleIds.Count > 0)
            query.Where(x => request.ScheduleIds.Contains(x.ScheduleId));

        request.ShiftDepartmentIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.ShiftDepartmentIds.Count > 0)
            query.Where(x => request.ShiftDepartmentIds.Contains(x.ShiftDepartmentName));

        request.ShiftUserIds.RemoveAll(x => string.IsNullOrWhiteSpace(x));

        if (request.ShiftUserIds.Count > 0)
            query.Where(x => request.ShiftUserIds.Contains(x.ShiftUserId));

        var entity = await query.CountAsync(cancellationToken);

        if (entity > 0)
            return new BaseResult(500, "数据重复");

        return new BaseResult();
    }
}
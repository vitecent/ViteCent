/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairSchedule;

/// <summary>
/// 补卡申请分页
/// </summary>
/// <param name="logger"></param>
public class PageRepairSchedule(ILogger<PageRepairSchedule> logger) : BaseDomain<RepairScheduleEntity>, IRequestHandler<SearchRepairScheduleEntityArgs, List<RepairScheduleEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 补卡申请分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<RepairScheduleEntity>> Handle(SearchRepairScheduleEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairSchedule.PageRepairSchedule");

        return await base.PageAsync(request);
    }
}
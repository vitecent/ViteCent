#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairSchedule;

/// <summary>
/// 编辑补卡申请
/// </summary>
/// <param name="logger"></param>
public class EditRepairSchedule(ILogger<EditRepairSchedule> logger) : BaseDomain<RepairScheduleEntity>, IRequestHandler<RepairScheduleEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 编辑补卡申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(RepairScheduleEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairSchedule.EditRepairSchedule");

        return await base.EditAsync(request);
    }
}
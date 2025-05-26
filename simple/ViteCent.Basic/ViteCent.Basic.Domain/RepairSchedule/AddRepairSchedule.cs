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
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairSchedule;

/// <summary>
/// 新增补卡申请领域
/// </summary>
/// <param name="logger"></param>
public class AddRepairSchedule(
    ILogger<AddRepairSchedule> logger)
    : BaseDomain<AddRepairScheduleEntity>, IRequestHandler<AddRepairScheduleEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 新增补卡申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddRepairScheduleEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairSchedule.AddRepairSchedule");

        return await base.AddAsync(request);
    }
}
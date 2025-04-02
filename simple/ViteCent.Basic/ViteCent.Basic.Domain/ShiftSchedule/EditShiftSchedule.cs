/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.ShiftSchedule;

/// <summary>
/// 编辑换班申请
/// </summary>
/// <param name="logger"></param>
public class EditShiftSchedule(ILogger<EditShiftSchedule> logger) : BaseDomain<ShiftScheduleEntity>, IRequestHandler<ShiftScheduleEntity, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 编辑换班申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(ShiftScheduleEntity request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ShiftSchedule.EditShiftSchedule");

        return await base.EditAsync(request);
    }
}
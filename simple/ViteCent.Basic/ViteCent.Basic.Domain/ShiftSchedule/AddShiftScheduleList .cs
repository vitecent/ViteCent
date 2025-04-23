/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
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
/// 批量新增换班申请
/// </summary>
/// <param name="logger"></param>
public class AddShiftScheduleList(
    ILogger<AddShiftScheduleList> logger)
    : BaseDomain<AddShiftScheduleEntity>, IRequestHandler<AddShiftScheduleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 批量新增换班申请
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddShiftScheduleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ShiftSchedule.AddShiftScheduleList");

        return await base.AddAsync(request.Items);
    }
}
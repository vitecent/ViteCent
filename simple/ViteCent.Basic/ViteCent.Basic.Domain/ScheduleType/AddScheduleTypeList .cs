/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.ScheduleType;

/// <summary>
/// 批量新增基础排班
/// </summary>
/// <param name="logger"></param>
public class AddScheduleTypeList(
    ILogger<AddScheduleTypeList> logger)
    : BaseDomain<AddScheduleTypeEntity>, IRequestHandler<AddScheduleTypeEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 批量新增基础排班
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddScheduleTypeEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ScheduleType.AddScheduleTypeList");

        return await base.AddAsync(request.Items);
    }
}
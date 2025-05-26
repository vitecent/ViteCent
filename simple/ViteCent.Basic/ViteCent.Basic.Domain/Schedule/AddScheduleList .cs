/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
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
/// 批量新增排班信息领域
/// </summary>
/// <param name="logger"></param>
public class AddScheduleList(
    ILogger<AddScheduleList> logger)
    : BaseDomain<AddScheduleEntity>, IRequestHandler<AddScheduleEntityListArgs, BaseResult>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 批量新增排班信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResult> Handle(AddScheduleEntityListArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.AddScheduleList");

        return await base.AddAsync(request.Items);
    }
}
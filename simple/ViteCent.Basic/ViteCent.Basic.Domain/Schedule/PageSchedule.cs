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
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.Schedule;

/// <summary>
/// 排班信息分页领域
/// </summary>
/// <param name="logger"></param>
public class PageSchedule(ILogger<PageSchedule> logger) : BaseDomain<ScheduleEntity>, IRequestHandler<SearchScheduleEntityArgs, List<ScheduleEntity>>
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// 排班信息分页
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<ScheduleEntity>> Handle(SearchScheduleEntityArgs request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.Schedule.PageSchedule");

        return await base.PageAsync(request);
    }
}
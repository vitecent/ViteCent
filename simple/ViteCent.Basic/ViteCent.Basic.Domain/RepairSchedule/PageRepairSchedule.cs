#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.RepairSchedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageRepairSchedule(ILogger<PageRepairSchedule> logger) : BaseDomain<RepairScheduleEntity>, IRequestHandler<SearchRepairScheduleEntityArgs, List<RepairScheduleEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<RepairScheduleEntity>> Handle(SearchRepairScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.RepairSchedule.PageRepairSchedule");

        return await base.PageAsync(request);
    }
}
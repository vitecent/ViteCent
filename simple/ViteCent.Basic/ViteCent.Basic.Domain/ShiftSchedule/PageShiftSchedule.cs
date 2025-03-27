#region

using MediatR;
using Microsoft.Extensions.Logging;
using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Domain.ShiftSchedule;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class PageShiftSchedule(ILogger<PageShiftSchedule> logger) : BaseDomain<ShiftScheduleEntity>, IRequestHandler<SearchShiftScheduleEntityArgs, List<ShiftScheduleEntity>>
{
    /// <summary>
    /// </summary>
    public override string DataBaseName => "ViteCent.Basic";

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<ShiftScheduleEntity>> Handle(SearchShiftScheduleEntityArgs request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Invoke ViteCent.Basic.Domain.ShiftSchedule.PageShiftSchedule");

        return await base.PageAsync(request);
    }
}
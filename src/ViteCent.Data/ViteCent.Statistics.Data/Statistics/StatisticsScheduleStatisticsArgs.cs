#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Statistics.Data.Statistics;

/// <summary>
/// </summary>
public class StatisticsScheduleStatisticsArgs : BaseArgs, IRequest<DataResult<ScheduleStatisticsResult>>
{
    /// <summary>
    /// </summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public StatisticsScheduleTypeEnum Type { get; set; }
}
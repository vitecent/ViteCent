#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Statistics.Data.Statistics;

/// <summary>
/// </summary>
public class StatisticsScheduleStatisticsEntityArgs : BaseArgs, IRequest<DataResult<ScheduleStatisticsResult>>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// </summary>
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Query { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// </summary>
    public string Type { get; set; } = string.Empty;
}
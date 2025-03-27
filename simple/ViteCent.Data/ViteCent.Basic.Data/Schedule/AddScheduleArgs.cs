#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class AddScheduleArgs : BaseArgs, IRequest<BaseResult>
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
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public bool Overnight { get; set; }

    /// <summary>
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
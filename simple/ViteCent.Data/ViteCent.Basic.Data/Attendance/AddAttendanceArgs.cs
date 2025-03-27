#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Attendance;

/// <summary>
/// </summary>
[Serializable]
public class AddAttendanceArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? FirstTime { get; set; }

    /// <summary>
    /// </summary>
    public DateTime? LastTime { get; set; }

    /// <summary>
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
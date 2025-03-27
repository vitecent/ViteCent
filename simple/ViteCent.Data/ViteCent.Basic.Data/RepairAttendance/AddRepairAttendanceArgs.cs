#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public class AddRepairAttendanceArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime RepairTime { get; set; }

    /// <summary>
    /// </summary>
    public int RepairType { get; set; }

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
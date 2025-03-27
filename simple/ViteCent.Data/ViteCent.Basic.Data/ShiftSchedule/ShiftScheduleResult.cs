namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class ShiftScheduleResult
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// </summary>
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
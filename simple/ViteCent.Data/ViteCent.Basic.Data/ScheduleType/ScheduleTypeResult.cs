namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public class ScheduleTypeResult
{
    /// <summary>
    /// </summary>
    public string Code { get; set; } = string.Empty;

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
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

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
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}
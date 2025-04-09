namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public class PreAddScheduleArgs
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 排班时间
    /// </summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 排班名称
    /// </summary>
    public string Shift { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称
    /// </summary>
    public string Job { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 职位标识
    /// </summary>
    public string PositionId { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
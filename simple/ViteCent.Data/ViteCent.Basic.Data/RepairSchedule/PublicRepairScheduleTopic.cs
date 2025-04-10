using MediatR;

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
public class PublicRepairScheduleTopic
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public RepairScheduleEnum Status { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
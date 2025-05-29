#region

using MediatR;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
public class ShiftScheduleTopicArgs : INotification
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
    /// 排班标识
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftDepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftPostName { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftUserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftPostId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftTypeId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftTypeName { get; set; } = string.Empty;
}
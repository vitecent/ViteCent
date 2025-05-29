/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// 换班申请
/// </summary>
[Serializable]
public class ShiftScheduleResult
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? Creator { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 岗位标识
    /// </summary>
    public string PostId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? PostName { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 排班标识
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门名称
    /// </summary>
    public string? ShiftDepartmentName { get; set; }

    /// <summary>
    /// 换班岗位标识
    /// </summary>
    public string ShiftPostId { get; set; } = string.Empty;

    /// <summary>
    /// 换班岗位名称
    /// </summary>
    public string? ShiftPostName { get; set; }

    /// <summary>
    /// 换班班次标识
    /// </summary>
    public string ShiftTypeId { get; set; } = string.Empty;

    /// <summary>
    /// 换班班次名称
    /// </summary>
    public string ShiftTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户名称
    /// </summary>
    public string? ShiftUserName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 班次标识
    /// </summary>
    public string TypeId { get; set; } = string.Empty;

    /// <summary>
    /// 班次名称
    /// </summary>
    public string TypeName { get; set; } = string.Empty;

    /// <summary>
    /// 修改人
    /// </summary>
    public string? Updater { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    public string? UserName { get; set; }
}
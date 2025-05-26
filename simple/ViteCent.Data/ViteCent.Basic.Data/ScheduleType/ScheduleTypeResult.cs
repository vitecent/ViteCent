/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// 基础排班
/// </summary>
[Serializable]
public class ScheduleTypeResult
{
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 颜色
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string Creator { get; set; } = string.Empty;

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
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 简介
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 结束时间
    /// </summary>
    public string EndTime { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否跨天
    /// </summary>
    public int Overnight { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public int ScheduleType { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public string StartTime { get; set; } = string.Empty;

    /// <summary>
    /// 修改人
    /// </summary>
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}
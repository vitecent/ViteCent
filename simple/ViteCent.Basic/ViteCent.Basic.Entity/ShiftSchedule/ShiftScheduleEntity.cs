/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 换班申请模型
/// </summary>
[Serializable]
[SugarTable("shift_schedule")]
public class ShiftScheduleEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "companyName")]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "createTime")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator")]
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion")]
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    [SugarColumn(ColumnName = "departmentId")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "departmentName")]
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "job")]
    public string Job { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "remark")]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 排班标识
    /// </summary>
    [SugarColumn(ColumnName = "scheduleId")]
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 排班名称
    /// </summary>
    [SugarColumn(ColumnName = "scheduleName")]
    public string ScheduleName { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftDepartmentId")]
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftDepartmentName")]
    public string ShiftDepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 换班岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftJob")]
    public string ShiftJob { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftUserId")]
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftUserName")]
    public string ShiftUserName { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int Status { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater")]
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    [SugarColumn(ColumnName = "userId")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnName = "userName")]
    public string UserName { get; set; } = string.Empty;
}
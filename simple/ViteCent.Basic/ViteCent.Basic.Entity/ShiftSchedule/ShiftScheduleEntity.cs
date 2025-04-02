/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 换班申请
/// </summary>
[Serializable]
[SugarTable("shift_schedule")]
public class ShiftScheduleEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "公司标识")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "createTime", IsNullable = true, ColumnDataType = "datetime", ColumnDescription = "创建时间")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "创建人")]
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion", ColumnDataType = "timestamp", ColumnDescription = "数据版本")]
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    [SugarColumn(ColumnName = "departmentId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "部门标识")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, ColumnDescription = "标识", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "remark", IsNullable = true, ColumnDataType = "varchar", Length = 5000, ColumnDescription = "描述")]
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 排班标识
    /// </summary>
    [SugarColumn(ColumnName = "scheduleId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "排班标识")]
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftDepartmentId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "换班部门标识")]
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 换班排班标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftScheduleId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "换班排班标识")]
    public string ShiftScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftUserId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "换班用户标识")]
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", IsNullable = true, ColumnDataType = "int", Length = 11, ColumnDescription = "状态")]
    public int Status { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "修改人")]
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime", IsNullable = true, ColumnDataType = "datetime", ColumnDescription = "修改时间")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    [SugarColumn(ColumnName = "userId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "用户标识")]
    public string UserId { get; set; } = string.Empty;
}
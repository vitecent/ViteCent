#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// 排班信息
/// </summary>
[Serializable]
[SugarTable("schedule")]
public class ScheduleEntity : BaseEntity, IRequest<BaseResult>
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
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "endTime", ColumnDataType = "datetime", ColumnDescription = "结束时间")]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 上班时间
    /// </summary>
    [SugarColumn(ColumnName = "firstTime", ColumnDataType = "datetime", ColumnDescription = "上班时间")]
    public DateTime FirstTime { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, ColumnDescription = "标识", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 下班时间
    /// </summary>
    [SugarColumn(ColumnName = "lastTime", ColumnDataType = "datetime", ColumnDescription = "下班时间")]
    public DateTime LastTime { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDataType = "varchar", Length = 100, ColumnDescription = "名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "startTime", ColumnDataType = "datetime", ColumnDescription = "开始时间")]
    public DateTime StartTime { get; set; }

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
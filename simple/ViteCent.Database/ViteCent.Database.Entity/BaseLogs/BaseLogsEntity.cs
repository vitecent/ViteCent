/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

// 引入 MediatR 用于实现中介者模式
using MediatR;

// 引入SqlSugar基础设施
using SqlSugar;

// 引入核心数据类型
using ViteCent.Core.Data;

// 引入ORM基础设施
using ViteCent.Core.Orm.SqlSugar;

#endregion 引入命名空间

namespace ViteCent.Database.Entity.BaseLogs;

/// <summary>
/// 日志信息模型
/// </summary>
[Serializable]
[SplitTable(SplitType.Year)]
[SugarTable("base_logs_{year}{month}{day}")]
public class BaseLogsEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 数据
    /// </summary>
    [SugarColumn(ColumnName = "args", ColumnDataType = "text", IsNullable = true, ColumnDescription = "数据")]
    public string? Args { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "公司标识")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "companyName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "公司名称")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SplitField]
    [SugarColumn(ColumnName = "createTime", ColumnDataType = "datetime", IsNullable = true, ColumnDescription = "创建时间")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "创建人")]
    public string? Creator { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    [SugarColumn(ColumnName = "departmentId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "部门标识")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "departmentName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "部门名称")]
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDataType = "varchar", Length = 5000, IsNullable = true, ColumnDescription = "简介")]
    public string? Description { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "标识")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 操作标识
    /// </summary>
    [SugarColumn(ColumnName = "operationId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "操作标识")]
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称
    /// </summary>
    [SugarColumn(ColumnName = "operationName", ColumnDataType = "varchar", Length = 50, ColumnDescription = "操作名称")]
    public string OperationName { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    [SugarColumn(ColumnName = "resourceId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "资源标识")]
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称
    /// </summary>
    [SugarColumn(ColumnName = "resourceName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "资源名称")]
    public string? ResourceName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "状态")]
    public int? Status { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    [SugarColumn(ColumnName = "systemId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "系统标识")]
    public string SystemId { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称
    /// </summary>
    [SugarColumn(ColumnName = "systemName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "系统名称")]
    public string? SystemName { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "修改人")]
    public string? Updater { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime", ColumnDataType = "datetime", IsNullable = true, ColumnDescription = "修改时间")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDataType = "timestamp", ColumnDescription = "数据版本", IsEnableUpdateVersionValidation = true)]
    public DateTime Version { get; set; }
}
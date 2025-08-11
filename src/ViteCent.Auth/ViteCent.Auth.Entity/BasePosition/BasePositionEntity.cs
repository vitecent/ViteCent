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

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// 职位信息模型
/// </summary>
[Serializable]
[SugarTable("base_position")]
public class BasePositionEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "abbreviation", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "简称")]
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "编码")]
    public string? Code { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    [SugarColumn(ColumnName = "color", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "颜色")]
    public string? Color { get; set; }

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
    [SugarColumn(ColumnName = "createTime", ColumnDataType = "datetime", IsNullable = true, ColumnDescription = "创建时间")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "创建人")]
    public string? Creator { get; set; }

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
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDataType = "varchar", Length = 100, ColumnDescription = "名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "sort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "排序")]
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "状态")]
    public int? Status { get; set; }

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
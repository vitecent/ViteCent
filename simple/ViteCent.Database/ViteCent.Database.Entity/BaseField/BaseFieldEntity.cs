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

namespace ViteCent.Database.Entity.BaseField;

/// <summary>
/// 表字段信息模型
/// </summary>
[Serializable]
[SugarTable("base_field")]
public class BaseFieldEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "abbreviation", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "简称")]
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 新增是否可见
    /// </summary>
    [SugarColumn(ColumnName = "add", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "新增是否可见")]
    public byte? Add { get; set; }

    /// <summary>
    /// 新增排序
    /// </summary>
    [SugarColumn(ColumnName = "addSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "新增排序")]
    public int? AddSort { get; set; }

    /// <summary>
    /// 新增宽度
    /// </summary>
    [SugarColumn(ColumnName = "addWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "新增宽度")]
    public int? AddWidth { get; set; }

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
    /// 详情是否可见
    /// </summary>
    [SugarColumn(ColumnName = "detail", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "详情是否可见")]
    public byte? Detail { get; set; }

    /// <summary>
    /// 详情排序
    /// </summary>
    [SugarColumn(ColumnName = "detailSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "详情排序")]
    public int? DetailSort { get; set; }

    /// <summary>
    /// 详情宽度
    /// </summary>
    [SugarColumn(ColumnName = "detailWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "详情宽度")]
    public int? DetailWidth { get; set; }

    /// <summary>
    /// 编辑是否可见
    /// </summary>
    [SugarColumn(ColumnName = "edit", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "编辑是否可见")]
    public byte? Edit { get; set; }

    /// <summary>
    /// 编辑排序
    /// </summary>
    [SugarColumn(ColumnName = "editSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "编辑排序")]
    public int? EditSort { get; set; }

    /// <summary>
    /// 编辑宽度
    /// </summary>
    [SugarColumn(ColumnName = "editWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "编辑宽度")]
    public int? EditWidth { get; set; }

    /// <summary>
    /// 导出是否可见
    /// </summary>
    [SugarColumn(ColumnName = "export", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "导出是否可见")]
    public byte? Export { get; set; }

    /// <summary>
    /// 导出排序
    /// </summary>
    [SugarColumn(ColumnName = "exportSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "导出排序")]
    public int? ExportSort { get; set; }

    /// <summary>
    /// 导出宽度
    /// </summary>
    [SugarColumn(ColumnName = "exportWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "导出宽度")]
    public int? ExportWidth { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "标识")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 是否自增
    /// </summary>
    [SugarColumn(ColumnName = "identity", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "是否自增")]
    public byte? Identity { get; set; }

    /// <summary>
    /// 导入是否可见
    /// </summary>
    [SugarColumn(ColumnName = "import", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "导入是否可见")]
    public byte? Import { get; set; }

    /// <summary>
    /// 导入排序
    /// </summary>
    [SugarColumn(ColumnName = "importSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "导入排序")]
    public int? ImportSort { get; set; }

    /// <summary>
    /// 导入宽度
    /// </summary>
    [SugarColumn(ColumnName = "importWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "导入宽度")]
    public int? ImportWidth { get; set; }

    /// <summary>
    /// 是否索引
    /// </summary>
    [SugarColumn(ColumnName = "index", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "是否索引")]
    public int? Index { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    [SugarColumn(ColumnName = "length", ColumnDataType = "int", Length = 11, ColumnDescription = "长度")]
    public int Length { get; set; }

    /// <summary>
    /// 下拉是否可见
    /// </summary>
    [SugarColumn(ColumnName = "list", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "下拉是否可见")]
    public byte? List { get; set; }

    /// <summary>
    /// 下拉排序
    /// </summary>
    [SugarColumn(ColumnName = "listSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "下拉排序")]
    public int? ListSort { get; set; }

    /// <summary>
    /// 下拉宽度
    /// </summary>
    [SugarColumn(ColumnName = "listWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "下拉宽度")]
    public int? ListWidth { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDataType = "varchar", Length = 100, ColumnDescription = "名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否主键
    /// </summary>
    [SugarColumn(ColumnName = "primaryKey", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "是否主键")]
    public byte? PrimaryKey { get; set; }

    /// <summary>
    /// 打印是否可见
    /// </summary>
    [SugarColumn(ColumnName = "print", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "打印是否可见")]
    public byte? Print { get; set; }

    /// <summary>
    /// 打印排序
    /// </summary>
    [SugarColumn(ColumnName = "printSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "打印排序")]
    public int? PrintSort { get; set; }

    /// <summary>
    /// 打印宽度
    /// </summary>
    [SugarColumn(ColumnName = "printWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "打印宽度")]
    public int? PrintWidth { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "sort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "排序")]
    public int? Sort { get; set; }

    /// <summary>
    /// 是否分表字段
    /// </summary>
    [SugarColumn(ColumnName = "splitField", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "是否分表字段")]
    public byte? SplitField { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "状态")]
    public int? Status { get; set; }

    /// <summary>
    /// 列表是否可见
    /// </summary>
    [SugarColumn(ColumnName = "table", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "列表是否可见")]
    public byte? Table { get; set; }

    /// <summary>
    /// 列表排序
    /// </summary>
    [SugarColumn(ColumnName = "tableSort", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "列表排序")]
    public int? TableSort { get; set; }

    /// <summary>
    /// 列表宽度
    /// </summary>
    [SugarColumn(ColumnName = "tableWidth", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "列表宽度")]
    public int? TableWidth { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "type", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "类型")]
    public string? Type { get; set; }

    /// <summary>
    /// 是否唯一
    /// </summary>
    [SugarColumn(ColumnName = "unique", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "是否唯一")]
    public byte? Unique { get; set; }

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

    /// <summary>
    /// 是否版本字段
    /// </summary>
    [SugarColumn(ColumnName = "versionField", ColumnDataType = "tinyint", Length = 1, IsNullable = true, ColumnDescription = "是否版本字段")]
    public byte? VersionField { get; set; }
}
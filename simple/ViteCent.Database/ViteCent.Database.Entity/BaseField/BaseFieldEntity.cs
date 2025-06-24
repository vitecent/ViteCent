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
    [SugarColumn(ColumnName = "abbreviation")]
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 新增是否可见
    /// </summary>
    [SugarColumn(ColumnName = "add")]
    public int? Add { get; set; }

    /// <summary>
    /// 新增排序
    /// </summary>
    [SugarColumn(ColumnName = "addSort")]
    public int? AddSort { get; set; }

    /// <summary>
    /// 新增宽度
    /// </summary>
    [SugarColumn(ColumnName = "addWidth")]
    public int? AddWidth { get; set; }

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "code")]
    public string? Code { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    [SugarColumn(ColumnName = "color")]
    public string? Color { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "companyName")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "createTime")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator")]
    public string? Creator { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "dataVersion")]
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    [SugarColumn(ColumnName = "description")]
    public string? Description { get; set; }

    /// <summary>
    /// 详情是否可见
    /// </summary>
    [SugarColumn(ColumnName = "detail")]
    public int? Detail { get; set; }

    /// <summary>
    /// 详情排序
    /// </summary>
    [SugarColumn(ColumnName = "detailSort")]
    public int? DetailSort { get; set; }

    /// <summary>
    /// 详情宽度
    /// </summary>
    [SugarColumn(ColumnName = "detailWidth")]
    public int? DetailWidth { get; set; }

    /// <summary>
    /// 编辑是否可见
    /// </summary>
    [SugarColumn(ColumnName = "edit")]
    public int? Edit { get; set; }

    /// <summary>
    /// 编辑排序
    /// </summary>
    [SugarColumn(ColumnName = "editSort")]
    public int? EditSort { get; set; }

    /// <summary>
    /// 编辑宽度
    /// </summary>
    [SugarColumn(ColumnName = "editWidth")]
    public int? EditWidth { get; set; }

    /// <summary>
    /// 导出是否可见
    /// </summary>
    [SugarColumn(ColumnName = "export")]
    public int? Export { get; set; }

    /// <summary>
    /// 导出排序
    /// </summary>
    [SugarColumn(ColumnName = "exportSort")]
    public int? ExportSort { get; set; }

    /// <summary>
    /// 导出宽度
    /// </summary>
    [SugarColumn(ColumnName = "exportWidth")]
    public int? ExportWidth { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 是否自增
    /// </summary>
    [SugarColumn(ColumnName = "identity")]
    public int? Identity { get; set; }

    /// <summary>
    /// 导入是否可见
    /// </summary>
    [SugarColumn(ColumnName = "import")]
    public int? Import { get; set; }

    /// <summary>
    /// 导入排序
    /// </summary>
    [SugarColumn(ColumnName = "importSort")]
    public int? ImportSort { get; set; }

    /// <summary>
    /// 导入宽度
    /// </summary>
    [SugarColumn(ColumnName = "importWidth")]
    public int? ImportWidth { get; set; }

    /// <summary>
    /// 是否索引
    /// </summary>
    [SugarColumn(ColumnName = "index")]
    public int? Index { get; set; }

    /// <summary>
    /// 长度
    /// </summary>
    [SugarColumn(ColumnName = "length")]
    public int Length { get; set; }

    /// <summary>
    /// 下拉是否可见
    /// </summary>
    [SugarColumn(ColumnName = "list")]
    public int? List { get; set; }

    /// <summary>
    /// 下拉排序
    /// </summary>
    [SugarColumn(ColumnName = "listSort")]
    public int? ListSort { get; set; }

    /// <summary>
    /// 下拉宽度
    /// </summary>
    [SugarColumn(ColumnName = "listWidth")]
    public int? ListWidth { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否主键
    /// </summary>
    [SugarColumn(ColumnName = "primaryKey")]
    public int? PrimaryKey { get; set; }

    /// <summary>
    /// 打印是否可见
    /// </summary>
    [SugarColumn(ColumnName = "print")]
    public int? Print { get; set; }

    /// <summary>
    /// 打印排序
    /// </summary>
    [SugarColumn(ColumnName = "printSort")]
    public int? PrintSort { get; set; }

    /// <summary>
    /// 打印宽度
    /// </summary>
    [SugarColumn(ColumnName = "printWidth")]
    public int? PrintWidth { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "sort")]
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int? Status { get; set; }

    /// <summary>
    /// 列表是否可见
    /// </summary>
    [SugarColumn(ColumnName = "table")]
    public int? Table { get; set; }

    /// <summary>
    /// 列表排序
    /// </summary>
    [SugarColumn(ColumnName = "tableSort")]
    public int? TableSort { get; set; }

    /// <summary>
    /// 列表宽度
    /// </summary>
    [SugarColumn(ColumnName = "tableWidth")]
    public int? TableWidth { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    [SugarColumn(ColumnName = "type")]
    public string? Type { get; set; }

    /// <summary>
    /// 是否唯一
    /// </summary>
    [SugarColumn(ColumnName = "unique")]
    public int? Unique { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater")]
    public string? Updater { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime")]
    public DateTime? UpdateTime { get; set; }
}
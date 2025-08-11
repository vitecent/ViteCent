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

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 公司信息模型
/// </summary>
[Serializable]
[SugarTable("base_company")]
public class BaseCompanyEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "abbreviation", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "简称")]
    public string? Abbreviation { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDataType = "varchar", Length = 500, IsNullable = true, ColumnDescription = "详细地址")]
    public string? Address { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnName = "city", ColumnDataType = "varchar", Length = 500, IsNullable = true, ColumnDescription = "城市")]
    public string? City { get; set; }

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
    /// 国家
    /// </summary>
    [SugarColumn(ColumnName = "country", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "国家")]
    public string? Country { get; set; }

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
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDataType = "varchar", Length = 100, IsNullable = true, ColumnDescription = "邮箱")]
    public string? Email { get; set; }

    /// <summary>
    /// 成立日期
    /// </summary>
    [SugarColumn(ColumnName = "establishDate", ColumnDataType = "date", IsNullable = true, ColumnDescription = "成立日期")]
    public DateTime? EstablishDate { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "标识")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 行业
    /// </summary>
    [SugarColumn(ColumnName = "industry", ColumnDataType = "varchar", Length = 500, IsNullable = true, ColumnDescription = "行业")]
    public string? Industry { get; set; }

    /// <summary>
    /// 法人
    /// </summary>
    [SugarColumn(ColumnName = "legalPerson", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "法人")]
    public string? LegalPerson { get; set; }

    /// <summary>
    /// 法人电话
    /// </summary>
    [SugarColumn(ColumnName = "legalPhone", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "法人电话")]
    public string? LegalPhone { get; set; }

    /// <summary>
    /// 级别
    /// </summary>
    [SugarColumn(ColumnName = "level", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "级别")]
    public string? Level { get; set; }

    /// <summary>
    /// 商标
    /// </summary>
    [SugarColumn(ColumnName = "logo", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "商标")]
    public string? Logo { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDataType = "varchar", Length = 100, ColumnDescription = "名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 父级标识
    /// </summary>
    [SugarColumn(ColumnName = "parentId", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "父级标识")]
    public string? ParentId { get; set; }

    /// <summary>
    /// 省份
    /// </summary>
    [SugarColumn(ColumnName = "province", ColumnDataType = "varchar", Length = 500, IsNullable = true, ColumnDescription = "省份")]
    public string? Province { get; set; }

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
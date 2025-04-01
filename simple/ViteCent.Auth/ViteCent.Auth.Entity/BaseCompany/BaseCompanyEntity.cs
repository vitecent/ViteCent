#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseCompany;

/// <summary>
/// 公司信息
/// </summary>
[Serializable]
[SugarTable("base_company")]
public class BaseCompanyEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "abbreviation", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "简称")]
    public string Abbreviation { get; set; } = string.Empty;

    /// <summary>
    /// 详细地址
    /// </summary>
    [SugarColumn(ColumnName = "address", IsNullable = true, ColumnDataType = "varchar", Length = 500, ColumnDescription = "详细地址")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// 城市
    /// </summary>
    [SugarColumn(ColumnName = "city", IsNullable = true, ColumnDataType = "varchar", Length = 500, ColumnDescription = "城市")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "code", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "编码")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 颜色
    /// </summary>
    [SugarColumn(ColumnName = "color", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "颜色")]
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// 国家
    /// </summary>
    [SugarColumn(ColumnName = "country", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "国家")]
    public string Country { get; set; } = string.Empty;

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
    /// 简介
    /// </summary>
    [SugarColumn(ColumnName = "description", IsNullable = true, ColumnDataType = "varchar", Length = 5000, ColumnDescription = "简介")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", IsNullable = true, ColumnDataType = "varchar", Length = 100, ColumnDescription = "邮箱")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 成立日期
    /// </summary>
    [SugarColumn(ColumnName = "establishDate", IsNullable = true, ColumnDataType = "date", ColumnDescription = "成立日期")]
    public DateTime? EstablishDate { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, ColumnDescription = "标识", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 行业
    /// </summary>
    [SugarColumn(ColumnName = "industry", IsNullable = true, ColumnDataType = "varchar", Length = 500, ColumnDescription = "行业")]
    public string Industry { get; set; } = string.Empty;

    /// <summary>
    /// 法人
    /// </summary>
    [SugarColumn(ColumnName = "legalPerson", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "法人")]
    public string LegalPerson { get; set; } = string.Empty;

    /// <summary>
    /// 法人电话
    /// </summary>
    [SugarColumn(ColumnName = "legalPhone", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "法人电话")]
    public string LegalPhone { get; set; } = string.Empty;

    /// <summary>
    /// 级别
    /// </summary>
    [SugarColumn(ColumnName = "level", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "级别")]
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// 商标
    /// </summary>
    [SugarColumn(ColumnName = "logo", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "商标")]
    public string Logo { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDataType = "varchar", Length = 100, ColumnDescription = "名称")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 父级标识
    /// </summary>
    [SugarColumn(ColumnName = "parentId", IsNullable = true, ColumnDataType = "varchar", Length = 50, ColumnDescription = "父级标识")]
    public string ParentId { get; set; } = string.Empty;

    /// <summary>
    /// 省份
    /// </summary>
    [SugarColumn(ColumnName = "province", IsNullable = true, ColumnDataType = "varchar", Length = 500, ColumnDescription = "省份")]
    public string Province { get; set; } = string.Empty;

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
}
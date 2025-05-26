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

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// 操作信息模型
/// </summary>
[Serializable]
[SugarTable("base_operation")]
public class BaseOperationEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "abbreviation")]
    public string Abbreviation { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    [SugarColumn(ColumnName = "code")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 颜色
    /// </summary>
    [SugarColumn(ColumnName = "color")]
    public string Color { get; set; } = string.Empty;

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
    /// 简介
    /// </summary>
    [SugarColumn(ColumnName = "description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 名称
    /// </summary>
    [SugarColumn(ColumnName = "name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    [SugarColumn(ColumnName = "resourceId")]
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称
    /// </summary>
    [SugarColumn(ColumnName = "resourceName")]
    public string ResourceName { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int Status { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    [SugarColumn(ColumnName = "systemId")]
    public string SystemId { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称
    /// </summary>
    [SugarColumn(ColumnName = "systemName")]
    public string SystemName { get; set; } = string.Empty;

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
}
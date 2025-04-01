#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// 角色权限
/// </summary>
[Serializable]
[SugarTable("base_role_permission")]
public class BaseRolePermissionEntity : BaseEntity, IRequest<BaseResult>
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
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, ColumnDescription = "标识", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 操作标识
    /// </summary>
    [SugarColumn(ColumnName = "operationId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "操作标识")]
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    [SugarColumn(ColumnName = "resourceId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "资源标识")]
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 角色标识
    /// </summary>
    [SugarColumn(ColumnName = "roleId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "角色标识")]
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", IsNullable = true, ColumnDataType = "int", Length = 11, ColumnDescription = "状态")]
    public int Status { get; set; }

    /// <summary>
    /// 系统标识
    /// </summary>
    [SugarColumn(ColumnName = "systemId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "系统标识")]
    public string SystemId { get; set; } = string.Empty;

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
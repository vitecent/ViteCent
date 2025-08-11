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

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// 调休申请模型
/// </summary>
[Serializable]
[SugarTable("user_rest")]
public class UserRestEntity : BaseEntity, IRequest<BaseResult>
{
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
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "endTime", ColumnDataType = "datetime", ColumnDescription = "结束时间")]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "标识")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDataType = "varchar", Length = 5000, IsNullable = true, ColumnDescription = "描述")]
    public string? Remark { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "startTime", ColumnDataType = "datetime", ColumnDescription = "开始时间")]
    public DateTime StartTime { get; set; }

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
    /// 用户标识
    /// </summary>
    [SugarColumn(ColumnName = "userId", ColumnDataType = "varchar", Length = 50, ColumnDescription = "用户标识")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnName = "userName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "用户名称")]
    public string? UserName { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDataType = "timestamp", ColumnDescription = "数据版本", IsEnableUpdateVersionValidation = true)]
    public DateTime Version { get; set; }
}
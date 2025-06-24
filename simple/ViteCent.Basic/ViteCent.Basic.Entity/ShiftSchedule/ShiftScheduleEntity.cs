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

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 换班申请模型
/// </summary>
[Serializable]
[SugarTable("shift_schedule")]
public class ShiftScheduleEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    [SugarColumn(ColumnName = "companyId")]
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    [SugarColumn(ColumnName = "companyName", IsNullable = true)]
    public string? CompanyName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "createTime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [SugarColumn(ColumnName = "creator", IsNullable = true)]
    public string? Creator { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "version", IsEnableUpdateVersionValidation = true)]
    public DateTime Version { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    [SugarColumn(ColumnName = "departmentId")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "departmentName", IsNullable = true)]
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 岗位标识
    /// </summary>
    [SugarColumn(ColumnName = "postId")]
    public string PostId { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "postName")]
    public string? PostName { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "remark")]
    public string? Remark { get; set; }

    /// <summary>
    /// 排班标识
    /// </summary>
    [SugarColumn(ColumnName = "scheduleId")]
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftDepartmentId")]
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftDepartmentName")]
    public string? ShiftDepartmentName { get; set; }

    /// <summary>
    /// 换班岗位标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftPostId")]
    public string ShiftPostId { get; set; } = string.Empty;

    /// <summary>
    /// 换班岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftPostName")]
    public string? ShiftPostName { get; set; }

    /// <summary>
    /// 换班班次标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftTypeId")]
    public string ShiftTypeId { get; set; } = string.Empty;

    /// <summary>
    /// 换班班次名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftTypeName")]
    public string ShiftTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    [SugarColumn(ColumnName = "shiftUserId")]
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户名称
    /// </summary>
    [SugarColumn(ColumnName = "shiftUserName")]
    public string? ShiftUserName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", IsNullable = true)]
    public int? Status { get; set; }

    /// <summary>
    /// 班次标识
    /// </summary>
    [SugarColumn(ColumnName = "typeId")]
    public string TypeId { get; set; } = string.Empty;

    /// <summary>
    /// 班次名称
    /// </summary>
    [SugarColumn(ColumnName = "typeName")]
    public string TypeName { get; set; } = string.Empty;

    /// <summary>
    /// 修改人
    /// </summary>
    [SugarColumn(ColumnName = "updater", IsNullable = true)]
    public string? Updater { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [SugarColumn(ColumnName = "updateTime")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    [SugarColumn(ColumnName = "userId")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// 用户名称
    /// </summary>
    [SugarColumn(ColumnName = "userName")]
    public string? UserName { get; set; }
}
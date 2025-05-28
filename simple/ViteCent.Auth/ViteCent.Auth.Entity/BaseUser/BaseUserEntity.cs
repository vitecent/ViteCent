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

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 用户信息模型
/// </summary>
[Serializable]
[SugarTable("base_user")]
public class BaseUserEntity : BaseEntity, IRequest<BaseResult>
{
    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar")]
    public string? Avatar { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "birthday")]
    public DateTime? Birthday { get; set; }

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
    /// 部门标识
    /// </summary>
    [SugarColumn(ColumnName = "departmentId")]
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "departmentName")]
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    [SugarColumn(ColumnName = "description")]
    public string? Description { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email")]
    public string? Email { get; set; }

    /// <summary>
    /// 指纹
    /// </summary>
    [SugarColumn(ColumnName = "finger")]
    public string? Finger { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "gender")]
    public int? Gender { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 身份证
    /// </summary>
    [SugarColumn(ColumnName = "idCard")]
    public string? IdCard { get; set; }

    /// <summary>
    /// 超级管理员
    /// </summary>
    [SugarColumn(ColumnName = "isSuper")]
    public int? IsSuper { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nickname")]
    public string? Nickname { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password")]
    public string? Password { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    [SugarColumn(ColumnName = "phone")]
    public string? Phone { get; set; }

    /// <summary>
    /// 职位标识
    /// </summary>
    [SugarColumn(ColumnName = "positionId")]
    public string? PositionId { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    [SugarColumn(ColumnName = "positionName")]
    public string? PositionName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "realName")]
    public string? RealName { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status")]
    public int? Status { get; set; }

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

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 编号
    /// </summary>
    [SugarColumn(ColumnName = "userNo")]
    public string? UserNo { get; set; }
}
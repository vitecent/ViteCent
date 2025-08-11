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
    [SugarColumn(ColumnName = "avatar", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "头像")]
    public string? Avatar { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [SugarColumn(ColumnName = "birthday", ColumnDataType = "date", IsNullable = true, ColumnDescription = "出生日期")]
    public DateTime? Birthday { get; set; }

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
    /// 指纹
    /// </summary>
    [SugarColumn(ColumnName = "finger", ColumnDataType = "varchar", Length = 4000, IsNullable = true, ColumnDescription = "指纹")]
    public string? Finger { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "性别")]
    public int? Gender { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDataType = "varchar", Length = 50, IsPrimaryKey = true, ColumnDescription = "标识")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 身份证
    /// </summary>
    [SugarColumn(ColumnName = "idCard", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "身份证")]
    public string? IdCard { get; set; }

    /// <summary>
    /// 超级管理员
    /// </summary>
    [SugarColumn(ColumnName = "isSuper", ColumnDataType = "int", Length = 11, IsNullable = true, ColumnDescription = "超级管理员")]
    public int? IsSuper { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nickname", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "昵称")]
    public string? Nickname { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "密码")]
    public string? Password { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    [SugarColumn(ColumnName = "phone", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "电话")]
    public string? Phone { get; set; }

    /// <summary>
    /// 职位标识
    /// </summary>
    [SugarColumn(ColumnName = "positionId", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "职位标识")]
    public string? PositionId { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    [SugarColumn(ColumnName = "positionName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "职位名称")]
    public string? PositionName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "realName", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "真实姓名")]
    public string? RealName { get; set; }

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
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "username", ColumnDataType = "varchar", Length = 50, ColumnDescription = "用户名")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 编号
    /// </summary>
    [SugarColumn(ColumnName = "userNo", ColumnDataType = "varchar", Length = 50, IsNullable = true, ColumnDescription = "编号")]
    public string? UserNo { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    [SugarColumn(ColumnName = "version", ColumnDataType = "timestamp", ColumnDescription = "数据版本", IsEnableUpdateVersionValidation = true)]
    public DateTime Version { get; set; }
}
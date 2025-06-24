/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// 用户信息
/// </summary>
[Serializable]
public class BaseUserResult
{
    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 颜色
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? Creator { get; set; }

    /// <summary>
    /// 数据版本
    /// </summary>
    public DateTime DataVersion { get; set; }

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 指纹
    /// </summary>
    public string? Finger { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public int? Gender { get; set; }

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 身份证
    /// </summary>
    public string? IdCard { get; set; }

    /// <summary>
    /// 超级管理员
    /// </summary>
    public int? IsSuper { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 职位标识
    /// </summary>
    public string? PositionId { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string? PositionName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string? RealName { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int? Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    public string? Updater { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 编号
    /// </summary>
    public string? UserNo { get; set; }
}
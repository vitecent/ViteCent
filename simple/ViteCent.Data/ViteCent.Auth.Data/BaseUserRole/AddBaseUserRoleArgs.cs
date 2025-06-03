/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 新增用户角色参数
/// </summary>
[Serializable]
public class AddBaseUserRoleArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 颜色
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 角色标识
    /// </summary>
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
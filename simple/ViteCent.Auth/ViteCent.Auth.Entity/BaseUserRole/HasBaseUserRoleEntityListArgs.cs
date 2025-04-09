/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// 批量用户角色判重参数
/// </summary>
[Serializable]
public class HasBaseUserRoleEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public List<string> Codes { get; set; } = [];

    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];

    /// <summary>
    /// 部门标识
    /// </summary>
    public List<string> DepartmentIds { get; set; } = [];

    /// <summary>
    /// </summary>
    public List<string> Names { get; set; } = [];

    /// <summary>
    /// 角色标识
    /// </summary>
    public List<string> RoleIds { get; set; } = [];

    /// <summary>
    /// 用户标识
    /// </summary>
    public List<string> UserIds { get; set; } = [];
}
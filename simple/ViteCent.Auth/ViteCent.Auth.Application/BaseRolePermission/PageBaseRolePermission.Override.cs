/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// 角色权限分页应用拓展
/// </summary>
public partial class PageBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseRolePermissionEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
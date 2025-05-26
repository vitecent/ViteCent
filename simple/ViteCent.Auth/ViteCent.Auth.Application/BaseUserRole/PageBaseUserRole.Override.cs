/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;

namespace ViteCent.Auth.Application.BaseUserRole;

/// <summary>
/// 用户角色分页应用拓展
/// </summary>
public partial class PageBaseUserRole
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseUserRoleEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
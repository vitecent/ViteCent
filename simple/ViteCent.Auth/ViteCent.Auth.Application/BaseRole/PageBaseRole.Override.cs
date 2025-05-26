/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Data;

namespace ViteCent.Auth.Application.BaseRole;

/// <summary>
/// 角色信息分页应用拓展
/// </summary>
public partial class PageBaseRole
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseRoleEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

namespace ViteCent.Auth.Application.BaseUser;

/// <summary>
/// 用户信息分页应用拓展
/// </summary>
public partial class PageBaseUser
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseUserEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
        args.AddArgs("IsSuper", "1", SearchEnum.NoEqual);
    }
}
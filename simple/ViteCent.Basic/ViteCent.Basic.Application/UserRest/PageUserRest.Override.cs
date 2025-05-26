/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.UserRest;

/// <summary>
/// 调休申请分页应用拓展
/// </summary>
public partial class PageUserRest
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchUserRestEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.UserLeave;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.UserLeave;

/// <summary>
/// 请假申请分页应用拓展
/// </summary>
public partial class PageUserLeave
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchUserLeaveEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
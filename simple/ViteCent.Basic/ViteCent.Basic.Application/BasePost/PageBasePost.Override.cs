/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.BasePost;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.BasePost;

/// <summary>
/// 职位信息分页应用拓展
/// </summary>
public partial class PageBasePost
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBasePostEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
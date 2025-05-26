/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;

namespace ViteCent.Auth.Application.BaseResource;

/// <summary>
/// 资源信息分页应用拓展
/// </summary>
public partial class PageBaseResource
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseResourceEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
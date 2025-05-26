/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Data;

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// 操作信息分页应用拓展
/// </summary>
public partial class PageBaseOperation
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchBaseOperationEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
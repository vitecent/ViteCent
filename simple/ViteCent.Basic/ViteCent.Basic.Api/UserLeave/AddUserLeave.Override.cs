/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Api.UserLeave;

/// <summary>
/// </summary>
public partial class AddUserLeave
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddUserLeaveArgs args, BaseUserInfo user)
    {
        args.Status = (int)UserLeaveEnum.Pass;

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
    }
}
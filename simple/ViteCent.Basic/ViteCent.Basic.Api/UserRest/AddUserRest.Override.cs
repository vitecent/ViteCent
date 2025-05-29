/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Api.UserRest;

/// <summary>
/// </summary>
public partial class AddUserRest
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddUserRestArgs args, BaseUserInfo user)
    {
        args.Status = (int)UserRestEnum.Pass;

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
    }
}
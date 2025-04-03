/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.UserLeave;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Enums;

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
    internal static void OverrideInvoke(AddUserRestArgs args, Core.Data.BaseUserInfo user)
    {
        args.Status = (int)UserRestEnum.Apply;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user.Company.Id;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = user.Department.Id;
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Api.UserLeave;

/// <summary>
/// </summary>
public partial class AddUserLeave
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private void OverrideInvoke(AddUserLeaveArgs args)
    {
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = User.Department.Id;
    }
}
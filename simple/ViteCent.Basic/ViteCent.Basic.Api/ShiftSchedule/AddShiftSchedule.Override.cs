/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.ShiftSchedule;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Api.ShiftSchedule;

/// <summary>
/// </summary>
public partial class AddShiftSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddShiftScheduleArgs args, BaseUserInfo user)
    {
        args.Status = (int)ShiftScheduleEnum.Pass;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = user.Company.Id;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = user.Department.Id;

        if (user.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.ShiftDepartmentId))
                args.ShiftDepartmentId = user.Department.Id;
    }
}
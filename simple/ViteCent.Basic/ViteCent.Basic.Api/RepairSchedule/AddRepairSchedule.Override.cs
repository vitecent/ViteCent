/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Api.RepairSchedule;

/// <summary>
/// </summary>
public partial class AddRepairSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    private void OverrideInvoke(AddRepairScheduleArgs args)
    {
        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.CompanyId))
                args.CompanyId = User.Company.Id;

        if (User.IsSuper != (int)YesNoEnum.Yes)
            if (string.IsNullOrEmpty(args.DepartmentId))
                args.DepartmentId = User.Department.Id;
    }
}
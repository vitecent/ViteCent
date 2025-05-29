/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Basic.Data.RepairSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Api.RepairSchedule;

/// <summary>
/// </summary>
public partial class AddRepairSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static void OverrideInvoke(AddRepairScheduleArgs args, BaseUserInfo user)
    {
        args.Status = (int)RepairScheduleEnum.Pass;

        if (string.IsNullOrEmpty(args.CompanyId))
            args.CompanyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrEmpty(args.DepartmentId))
            args.DepartmentId = user?.Department?.Id ?? string.Empty; ;
    }
}
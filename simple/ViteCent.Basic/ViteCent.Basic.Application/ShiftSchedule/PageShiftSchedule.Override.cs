/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.ShiftSchedule;

/// <summary>
/// 换班申请分页应用拓展
/// </summary>
public partial class PageShiftSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchShiftScheduleEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
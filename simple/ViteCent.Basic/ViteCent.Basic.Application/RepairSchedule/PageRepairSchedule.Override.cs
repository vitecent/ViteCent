/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.RepairSchedule;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.RepairSchedule;

/// <summary>
/// 补卡申请分页应用拓展
/// </summary>
public partial class PageRepairSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchRepairScheduleEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
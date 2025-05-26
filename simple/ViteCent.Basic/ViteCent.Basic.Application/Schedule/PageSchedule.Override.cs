/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.Schedule;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.Schedule;

/// <summary>
/// 排班信息分页应用拓展
/// </summary>
public partial class PageSchedule
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchScheduleEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
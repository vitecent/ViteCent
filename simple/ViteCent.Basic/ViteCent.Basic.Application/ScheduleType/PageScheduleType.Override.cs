/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

#endregion

using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// 基础排班分页应用拓展
/// </summary>
public partial class PageScheduleType
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <param name="user"></param>
    private void OverrideHandle(SearchScheduleTypeEntityArgs args, BaseUserInfo user)
    {
        args.AddCompanyId(user);
    }
}
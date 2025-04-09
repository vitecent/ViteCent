/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// 批量排班信息判重参数
/// </summary>
[Serializable]
public class HasScheduleEntityListArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public List<string> CompanyIds { get; set; } = [];

    /// <summary>
    /// 部门标识
    /// </summary>
    public List<string> DepartmentIds { get; set; } = [];

    /// <summary>
    /// 用户标识
    /// </summary>
    public List<string> UserIds { get; set; } = [];

    /// <summary>
    /// </summary>
    public DateTime EndTime { get; set; }

    /// <summary>
    /// </summary>
    public DateTime StartTime { get; set; }
}
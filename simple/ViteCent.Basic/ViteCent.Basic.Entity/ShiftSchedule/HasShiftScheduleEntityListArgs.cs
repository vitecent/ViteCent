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

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 批量换班申请判重参数
/// </summary>
[Serializable]
public class HasShiftScheduleEntityListArgs : BaseArgs, IRequest<BaseResult>
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
}
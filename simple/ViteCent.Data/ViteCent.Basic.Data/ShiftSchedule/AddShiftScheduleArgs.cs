#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// 新增换班申请参数
/// </summary>
[Serializable]
public class AddShiftScheduleArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string Remark { get; set; } = string.Empty;

    /// <summary>
    /// 排班标识
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班部门标识
    /// </summary>
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 换班排班标识
    /// </summary>
    public string ShiftScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// 换班用户标识
    /// </summary>
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
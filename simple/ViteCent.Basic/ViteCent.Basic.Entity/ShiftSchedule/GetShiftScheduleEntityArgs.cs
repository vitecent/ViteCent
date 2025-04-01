#region

using MediatR;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 获取换班申请数据参数
/// </summary>
[Serializable]
public class GetShiftScheduleEntityArgs : IRequest<ShiftScheduleEntity>
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
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
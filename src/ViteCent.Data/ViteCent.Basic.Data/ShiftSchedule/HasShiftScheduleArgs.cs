#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class HasShiftScheduleEntityArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ScheduleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftDepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ShiftUserId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
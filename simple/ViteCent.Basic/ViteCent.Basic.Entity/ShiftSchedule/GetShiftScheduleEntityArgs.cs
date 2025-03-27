#region

using MediatR;
using ViteCent.Basic.Entity.ShiftSchedule;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class GetShiftScheduleEntityArgs : IRequest<ShiftScheduleEntity>
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
    public string UserId { get; set; } = string.Empty;

}
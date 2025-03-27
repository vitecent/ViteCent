#region

using MediatR;
using ViteCent.Basic.Entity.RepairAttendance;

#endregion

namespace ViteCent.Basic.Entity.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public class GetRepairAttendanceEntityArgs : IRequest<RepairAttendanceEntity>
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
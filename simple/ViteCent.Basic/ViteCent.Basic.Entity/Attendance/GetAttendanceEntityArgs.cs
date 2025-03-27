#region

using MediatR;
using ViteCent.Basic.Entity.Attendance;

#endregion

namespace ViteCent.Basic.Entity.Attendance;

/// <summary>
/// </summary>
[Serializable]
public class GetAttendanceEntityArgs : IRequest<AttendanceEntity>
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
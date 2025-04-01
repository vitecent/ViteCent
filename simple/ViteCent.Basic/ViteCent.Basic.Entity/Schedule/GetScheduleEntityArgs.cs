#region

using MediatR;
using ViteCent.Basic.Entity.Schedule;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class GetScheduleEntityArgs : IRequest<ScheduleEntity>
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
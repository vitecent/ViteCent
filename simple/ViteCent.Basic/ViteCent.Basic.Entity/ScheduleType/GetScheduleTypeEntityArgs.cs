#region

using MediatR;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public class GetScheduleTypeEntityArgs : IRequest<ScheduleTypeEntity>
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
}
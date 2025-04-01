#region

using MediatR;
using ViteCent.Basic.Entity.RepairSchedule;

#endregion

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// </summary>
[Serializable]
public class GetRepairScheduleEntityArgs : IRequest<RepairScheduleEntity>
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
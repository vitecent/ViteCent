#region

using MediatR;
using ViteCent.Basic.Entity.UserLeave;

#endregion

namespace ViteCent.Basic.Entity.UserLeave;

/// <summary>
/// </summary>
[Serializable]
public class GetUserLeaveEntityArgs : IRequest<UserLeaveEntity>
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
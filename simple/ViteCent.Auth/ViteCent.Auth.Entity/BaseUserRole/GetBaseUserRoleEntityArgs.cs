#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseUserRoleEntityArgs : IRequest<BaseUserRoleEntity>
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
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
#region

using MediatR;
using ViteCent.Auth.Entity.BaseRolePermission;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseRolePermissionEntityArgs : IRequest<BaseRolePermissionEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string SystemId { get; set; } = string.Empty;

}
#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseRole;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseRoleEntityArgs : IRequest<BaseRoleEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
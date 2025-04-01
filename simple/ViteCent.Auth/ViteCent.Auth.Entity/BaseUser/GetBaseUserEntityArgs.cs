#region

using MediatR;
using ViteCent.Auth.Entity.BaseUser;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseUserEntityArgs : IRequest<BaseUserEntity>
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
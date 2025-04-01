#region

using MediatR;
using ViteCent.Auth.Entity.BaseSystem;

#endregion

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseSystemEntityArgs : IRequest<BaseSystemEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
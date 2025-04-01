#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseResource;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseResourceEntityArgs : IRequest<BaseResourceEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}
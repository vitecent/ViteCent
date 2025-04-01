#region

using MediatR;
using ViteCent.Auth.Entity.BasePosition;

#endregion

namespace ViteCent.Auth.Entity.BasePosition;

/// <summary>
/// </summary>
[Serializable]
public class GetBasePositionEntityArgs : IRequest<BasePositionEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
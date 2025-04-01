#region

using MediatR;

#endregion

namespace ViteCent.Auth.Entity.BaseOperation;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseOperationEntityArgs : IRequest<BaseOperationEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}
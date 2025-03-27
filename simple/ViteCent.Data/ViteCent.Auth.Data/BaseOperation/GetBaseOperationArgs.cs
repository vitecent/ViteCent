#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseOperationArgs : BaseArgs, IRequest<DataResult<BaseOperationResult>>
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
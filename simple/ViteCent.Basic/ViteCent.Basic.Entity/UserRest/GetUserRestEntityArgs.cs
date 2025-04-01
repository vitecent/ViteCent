#region

using MediatR;
using ViteCent.Basic.Entity.UserRest;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// </summary>
[Serializable]
public class GetUserRestEntityArgs : IRequest<UserRestEntity>
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
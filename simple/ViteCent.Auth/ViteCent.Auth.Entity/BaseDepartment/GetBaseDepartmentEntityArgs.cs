#region

using MediatR;
using ViteCent.Auth.Entity.BaseDepartment;

#endregion

namespace ViteCent.Auth.Entity.BaseDepartment;

/// <summary>
/// </summary>
[Serializable]
public class GetBaseDepartmentEntityArgs : IRequest<BaseDepartmentEntity>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

}
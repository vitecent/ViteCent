#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
public class GetAllPermissionArgs : BaseArgs, IRequest<DataResult<AllPermissionResult>>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;
}
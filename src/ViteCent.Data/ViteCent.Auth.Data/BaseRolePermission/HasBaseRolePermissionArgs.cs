/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
[Serializable]
public class HasBaseRolePermissionEntityArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}
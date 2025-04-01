#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// 删除角色权限数据参数
/// </summary>
[Serializable]
public class DeleteBaseRolePermissionEntityArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 操作标识
    /// </summary>
    public string OperationId { get; set; } = string.Empty;

    /// <summary>
    /// 资源标识
    /// </summary>
    public string ResourceId { get; set; } = string.Empty;

    /// <summary>
    /// 角色标识
    /// </summary>
    public string RoleId { get; set; } = string.Empty;

    /// <summary>
    /// 系统标识
    /// </summary>
    public string SystemId { get; set; } = string.Empty;
}
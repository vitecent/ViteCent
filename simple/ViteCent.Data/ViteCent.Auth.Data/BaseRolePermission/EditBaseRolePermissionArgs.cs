namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// 编辑角色权限参数
/// </summary>
[Serializable]
public class EditBaseRolePermissionArgs : AddBaseRolePermissionArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
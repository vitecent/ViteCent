namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseRolePermissionArgs : AddBaseRolePermissionArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
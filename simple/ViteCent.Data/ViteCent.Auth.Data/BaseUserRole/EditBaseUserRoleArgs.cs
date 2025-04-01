namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 编辑用户角色参数
/// </summary>
[Serializable]
public class EditBaseUserRoleArgs : AddBaseUserRoleArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
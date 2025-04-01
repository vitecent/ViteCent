namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// 编辑角色信息参数
/// </summary>
[Serializable]
public class EditBaseRoleArgs : AddBaseRoleArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
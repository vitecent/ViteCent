namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseUserRoleArgs : AddBaseUserRoleArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
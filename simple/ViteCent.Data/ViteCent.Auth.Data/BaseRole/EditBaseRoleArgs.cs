namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseRoleArgs : AddBaseRoleArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
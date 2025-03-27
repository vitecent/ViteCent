namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseUserArgs : AddBaseUserArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
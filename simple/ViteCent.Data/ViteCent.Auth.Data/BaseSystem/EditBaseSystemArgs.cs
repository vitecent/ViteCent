namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseSystemArgs : AddBaseSystemArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// </summary>
[Serializable]
public class EditBasePositionArgs : AddBasePositionArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
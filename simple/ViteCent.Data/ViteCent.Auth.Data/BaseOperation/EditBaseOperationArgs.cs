namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseOperationArgs : AddBaseOperationArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
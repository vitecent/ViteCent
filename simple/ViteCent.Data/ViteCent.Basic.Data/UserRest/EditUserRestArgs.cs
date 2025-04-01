namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// </summary>
[Serializable]
public class EditUserRestArgs : AddUserRestArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
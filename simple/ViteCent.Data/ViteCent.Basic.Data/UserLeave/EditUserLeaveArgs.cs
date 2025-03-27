namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// </summary>
[Serializable]
public class EditUserLeaveArgs : AddUserLeaveArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
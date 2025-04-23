namespace ViteCent.Auth.Data.Schedule;

/// <summary>
/// </summary>
public class UserFinger
{
    /// <summary>
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// </summary>
    public object Template { get; set; } = new();

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserName { get; set; } = string.Empty;
}
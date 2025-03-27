namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class LoginResult
{
    /// <summary>
    /// </summary>
    public DateTime ExpireTime { get; set; }

    /// <summary>
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
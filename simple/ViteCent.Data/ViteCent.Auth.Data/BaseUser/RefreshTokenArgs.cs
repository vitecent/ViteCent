#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class RefreshTokenArgs : BaseArgs
{
    /// <summary>
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class ChangePaswordArgs : BaseArgs
{
    /// <summary>
    /// </summary>
    public string OriginalPassword { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
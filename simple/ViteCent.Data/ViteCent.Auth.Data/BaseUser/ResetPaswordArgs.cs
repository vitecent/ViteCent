#region

using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class ResetPaswordArgs : BaseArgs
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Password { get; set; } = BaseConst.DefaultPassword;

    /// <summary>
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
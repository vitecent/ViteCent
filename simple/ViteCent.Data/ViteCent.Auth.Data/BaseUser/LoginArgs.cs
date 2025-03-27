#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class LoginArgs : BaseArgs, IRequest<DataResult<LoginResult>>
{
    /// <summary>
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Username { get; set; } = string.Empty;
}
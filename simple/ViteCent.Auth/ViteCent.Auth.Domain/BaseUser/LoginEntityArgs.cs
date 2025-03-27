#region

using MediatR;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Domain.BaseUser;

/// <summary>
/// </summary>
public class LoginEntityArgs : IRequest<BaseUserEntity>

{
    /// <summary>
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Username { get; set; } = string.Empty;
}
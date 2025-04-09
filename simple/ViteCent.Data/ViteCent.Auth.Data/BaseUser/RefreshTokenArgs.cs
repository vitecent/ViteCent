#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class RefreshTokenArgs : BaseArgs, IRequest<DataResult<RefreshTokenResult>>
{
    /// <summary>
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}
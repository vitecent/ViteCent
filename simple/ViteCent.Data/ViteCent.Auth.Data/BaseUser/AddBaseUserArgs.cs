#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class AddBaseUserArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// </summary>
    public string IdCard { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Nickname { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserNo { get; set; } = string.Empty;
}
#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class HasBaseUserEntityArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string IdCard { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserNo { get; set; } = string.Empty;
}
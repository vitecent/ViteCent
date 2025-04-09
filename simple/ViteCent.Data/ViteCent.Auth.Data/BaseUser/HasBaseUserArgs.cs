/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

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
    /// 职位标识
    /// </summary>
    public string PositionId { get; set; } = string.Empty;

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
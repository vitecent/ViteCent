#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public class AddBaseCompanyArgs : BaseArgs, IRequest<BaseResult>
{
    /// <summary>
    /// </summary>
    public string Abbreviation { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? EstablishDate { get; set; }

    /// <summary>
    /// </summary>
    public string Industry { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string LegalPerson { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string LegalPhone { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Level { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ParentId { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Province { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Status { get; set; }
}
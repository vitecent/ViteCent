namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public class BaseCompanyResult
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
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime DataVersion { get; set; }

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
    public string Id { get; set; } = string.Empty;

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

    /// <summary>
    /// </summary>
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}
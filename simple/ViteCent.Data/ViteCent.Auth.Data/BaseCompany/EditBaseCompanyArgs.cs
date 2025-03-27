namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseCompanyArgs : AddBaseCompanyArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
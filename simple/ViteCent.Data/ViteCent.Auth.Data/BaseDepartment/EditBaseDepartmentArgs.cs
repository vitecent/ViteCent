namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// </summary>
[Serializable]
public class EditBaseDepartmentArgs : AddBaseDepartmentArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
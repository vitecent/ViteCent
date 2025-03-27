namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class BaseUserResult
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
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// </summary>
    public string Creator { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime DataVersion { get; set; }

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
    public string Id { get; set; } = string.Empty;

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
    public string Updater { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string UserNo { get; set; } = string.Empty;
}
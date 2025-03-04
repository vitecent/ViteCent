namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class BaseUserInfo
{
    /// <summary>
    /// </summary>
    /// <value>The authentication.</value>
    public List<BaseSystem> Auth { get; set; } = [];

    /// <summary>
    /// </summary>
    /// <value>The code.</value>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The company.</value>
    public BaseCompany Company { get; set; } = new();

    /// <summary>
    /// </summary>
    /// <value>The dept.</value>
    public List<BaseDept> Depts { get; set; } = [];

    /// <summary>
    /// </summary>
    /// <value>The identifier.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The is super admin.</value>
    public int IsSuper { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
}
namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class BaseTable
{
    /// <summary>
    /// </summary>
    public List<BaseField> BaseFields { get; set; } = [];

    /// <summary>
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
}
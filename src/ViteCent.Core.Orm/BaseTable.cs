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
    public string Name { get; set; } = string.Empty;
}
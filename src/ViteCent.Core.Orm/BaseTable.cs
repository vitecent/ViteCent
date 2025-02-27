namespace ViteCent.Core.Orm;

/// <summary>
///     Class BaseTable.
/// </summary>
public class BaseTable
{
    /// <summary>
    ///     BaseFields
    /// </summary>
    public List<BaseField> BaseFields { get; set; } = [];

    /// <summary>
    ///     Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
}
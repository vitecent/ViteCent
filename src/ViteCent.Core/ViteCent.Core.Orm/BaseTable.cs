namespace ViteCent.Core.Orm;

/// <summary>
///     Class BaseTable.
/// </summary>
public class BaseTable
{
    /// <summary>
    ///     Gets or sets the DataBase identifier.
    /// </summary>
    /// <value>The DataBase identifier.</value>
    public string DataBaseName { get; set; } = string.Empty;

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
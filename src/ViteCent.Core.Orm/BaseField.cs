namespace ViteCent.Core.Orm;

/// <summary>
///     Class BaseField.
/// </summary>
public class BaseField
{
    /// <summary>
    ///     Gets or sets the DataBase identifier.
    /// </summary>
    /// <value>The DataBase identifier.</value>
    public string DataBaseName { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the table identifier.
    /// </summary>
    /// <value>The table identifier.</value>
    public string TableName { get; set; } = string.Empty;
}
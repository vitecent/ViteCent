namespace ViteCent.Core.Orm;

/// <summary>
///     Class BaseDataBase.
/// </summary>
public class BaseDataBase
{
    /// <summary>
    ///     BaseTables
    /// </summary>
    public List<BaseTable> BaseTables { get; set; } = [];

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
}
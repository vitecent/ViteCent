namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class BaseDataBase
{
    /// <summary>
    /// </summary>
    public List<BaseTable> BaseTables { get; set; } = [];

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;
}
namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class FactoryConfig
{
    /// <summary>
    /// </summary>
    /// <value>The type of the DataBase.</value>
    public string DbType { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The master.</value>
    public string Master { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The slaves.</value>
    public List<string> Slaves { get; set; } = [];
}
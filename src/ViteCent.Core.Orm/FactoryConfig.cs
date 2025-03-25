namespace ViteCent.Core.Orm;

/// <summary>
/// </summary>
public class FactoryConfig
{
    /// <summary>
    /// </summary>
    public string DbType { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Master { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public List<string> Slaves { get; set; } = [];
}
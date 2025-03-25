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
    public string Name { get; set; } = string.Empty;
}
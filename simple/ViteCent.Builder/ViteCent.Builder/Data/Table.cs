using ViteCent.Core.Orm;

namespace ViteCent.Builder.Data;

/// <summary>
/// </summary>
public class Table : BaseTable
{
    /// <summary>
    /// </summary>
    public string CamelCaseName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public List<Field> Fields { get; set; } = [];
}
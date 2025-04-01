using ViteCent.Core.Orm;

namespace ViteCent.Builder.Data;

/// <summary>
/// </summary>
public class Field : BaseField
{
    /// <summary>
    /// </summary>
    public string CamelCaseName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnDescription { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnIdentity { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnLength { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnNullable { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnPrimaryKey { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string ColumnType { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string DataType { get; set; } = string.Empty;
}
#region 引入命名空间

// 引入核心数据模型
using ViteCent.Core.Orm;

#endregion 引入命名空间

namespace ViteCent.Builder.Data.Build;

/// <summary>
/// </summary>
public class Field : BaseFieldInfo
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
#region 引入命名空间

// 引入核心数据模型
using ViteCent.Core.Orm;

#endregion 引入命名空间

namespace ViteCent.Builder.Data.Build;

/// <summary>
/// </summary>
public class Table : BaseTableInfo
{
    /// <summary>
    /// </summary>
    public string CamelCaseName { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public List<Field> Fields { get; set; } = [];
}
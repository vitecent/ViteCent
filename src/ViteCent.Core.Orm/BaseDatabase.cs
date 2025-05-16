namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库定义类，用于描述数据库的整体结构信息
/// </summary>
public class BaseDataBase
{
    /// <summary>
    /// 数据表集合，包含该数据库中所有表的定义信息
    /// </summary>
    public List<BaseTable> BaseTables { get; set; } = [];

    /// <summary>
    /// 数据库名称，对应实际的数据库名
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
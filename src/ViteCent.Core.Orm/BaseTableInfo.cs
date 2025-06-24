namespace ViteCent.Core.Orm;

/// <summary>
/// 数据表定义类，用于描述数据库表的参数信息
/// </summary>
public class BaseTableInfo
{
    /// <summary>
    /// 表字段集合，包含表中所有字段的定义信息
    /// </summary>
    public List<BaseFieldInfo> BaseFields { get; set; } = [];

    /// <summary>
    /// 表描述，用于说明该表的用途和功能
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 表名称，对应数据库中的实际表名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 编码，用于标识该表的唯一编码或标识符
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 分表类型，标识该表的分表策略或方式
    /// </summary>
    public string SplitType { get; set; } = "None";
}
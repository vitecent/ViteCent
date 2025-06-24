namespace ViteCent.Core.Orm;

/// <summary>
/// 数据表字段定义类，用于描述数据库表中单个字段的参数信息
/// </summary>
public class BaseFieldInfo
{
    /// <summary>
    /// 字段默认值，当插入数据未指定值时使用的默认值
    /// </summary>
    public string Default { get; set; } = string.Empty;

    /// <summary>
    /// 字段描述，用于说明该字段的用途和含义
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 是否为自增字段，标识该字段的值是否由数据库自动递增生成
    /// </summary>
    public bool Identity { get; set; }

    /// <summary>
    /// 字段长度，适用于字符串等类型字段的最大长度限制
    /// </summary>
    public int Length { get; set; }

    /// <summary>
    /// 字段名称，对应数据库中的实际列名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 是否允许为空，标识该字段是否可以存储null值
    /// </summary>
    public bool Nullable { get; set; }

    /// <summary>
    /// 是否为主键，标识该字段是否作为表的主键
    /// </summary>
    public bool PrimaryKey { get; set; }

    /// <summary>
    /// 字段类型，标识该字段在数据库中的数据类型
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// 编码，用于标识该字段的唯一编码或标识符
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 是否索引，标识该字段是否被索引以提高查询性能
    /// </summary>
    public bool Index { get; set; }

    /// <summary>
    /// 是否唯一，标识该字段的值在表中是否必须唯一
    /// </summary>
    public bool Unique { get; set; }

    /// <summary>
    /// 是否分表字段，标识该字段是否用于数据分表
    /// </summary>
    public bool SplitField { get; set; }

    /// <summary>
    /// 是否版本字段，标识该字段是否用于版本控制
    /// </summary>
    public bool VersionField { get; set; }
}
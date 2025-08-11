namespace ViteCent.Core.Enums;

/// <summary>
/// 查询比较方式枚举，用于定义数据查询时的比较操作类型
/// </summary>
public enum SearchEnum
{
    /// <summary>
    /// 等于，精确匹配指定的值
    /// </summary>
    Equal = 1,

    /// <summary>
    /// 模糊匹配，在字段值中包含指定的值
    /// </summary>
    Like = 2,

    /// <summary>
    /// 大于，字段值严格大于指定的值
    /// </summary>
    GreaterThan = 3,

    /// <summary>
    /// 大于等于，字段值大于或等于指定的值
    /// </summary>
    GreaterThanOrEqual = 4,

    /// <summary>
    /// 小于，字段值严格小于指定的值
    /// </summary>
    LessThan = 5,

    /// <summary>
    /// 小于等于，字段值小于或等于指定的值
    /// </summary>
    LessThanOrEqual = 6,

    /// <summary>
    /// 包含于，字段值在指定的值列表中
    /// </summary>
    In = 7,

    /// <summary>
    /// 不包含于，字段值不在指定的值列表中
    /// </summary>
    NotIn = 8,

    /// <summary>
    /// 左模糊匹配，字段值以指定的值结尾
    /// </summary>
    LikeLeft = 9,

    /// <summary>
    /// 右模糊匹配，字段值以指定的值开头
    /// </summary>
    LikeRight = 10,

    /// <summary>
    /// 不等于，字段值与指定的值不相等
    /// </summary>
    NoEqual = 11,

    /// <summary>
    /// 为空，字段值为null或空字符串
    /// </summary>
    IsNullOrEmpty = 12,

    /// <summary>
    /// 不为空，字段值不为null
    /// </summary>
    IsNot = 13,

    /// <summary>
    /// 不包含，字段值不包含指定的值
    /// </summary>
    NoLike = 14,

    /// <summary>
    /// 等于空，字段值为null
    /// </summary>
    EqualNull = 15,

    /// <summary>
    /// 包含匹配，在指定的值列表中进行模糊匹配
    /// </summary>
    InLike = 16
}
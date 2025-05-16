namespace ViteCent.Core.Enums;

/// <summary>
/// 数据操作类型枚举，用于定义数据查询和操作的方式
/// </summary>
public enum DataEnum
{
    /// <summary>
    /// 实体操作，通过实体对象进行数据操作
    /// </summary>
    Entity = 1,

    /// <summary>
    /// 条件操作，通过指定查询条件进行数据操作
    /// </summary>
    Where = 2,

    /// <summary>
    /// SQL操作，通过直接执行SQL语句进行数据操作
    /// </summary>
    SQL = 3
}
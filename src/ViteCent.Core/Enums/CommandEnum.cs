namespace ViteCent.Core.Enums;

/// <summary>
/// 数据操作类型枚举，用于定义数据库操作的基本类型
/// </summary>
public enum CommandEnum
{
    /// <summary>
    /// 插入操作，用于向数据库中添加新的记录
    /// </summary>
    Insert = 1,

    /// <summary>
    /// 更新操作，用于修改数据库中已存在的记录
    /// </summary>
    Update = 2,

    /// <summary>
    /// 删除操作，用于从数据库中移除指定的记录
    /// </summary>
    Delete = 3
}
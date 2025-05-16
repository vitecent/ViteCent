#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库命令操作类，用于定义和封装数据库操作的相关信息
/// </summary>
public class Command
{
    /// <summary>
    /// 命令类型，指定当前数据库操作的类型
    /// </summary>
    public CommandEnum CommandType { get; set; }

    /// <summary>
    /// 数据类型，指定操作数据的类型
    /// </summary>
    public DataEnum DataType { get; set; }

    /// <summary>
    /// 实体对象，用于存储要操作的数据实体
    /// </summary>
    public dynamic Entity { get; set; } = string.Empty;

    /// <summary>
    /// 参数对象，用于存储数据库操作的参数信息
    /// </summary>
    public object Parameters { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句，用于存储需要执行的SQL查询语句
    /// </summary>
    public string SQL { get; set; } = string.Empty;

    /// <summary>
    /// 条件表达式，用于指定数据库操作的查询条件
    /// </summary>
    public dynamic Where { get; set; } = string.Empty;
}
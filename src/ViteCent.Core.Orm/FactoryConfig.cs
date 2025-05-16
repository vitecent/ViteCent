namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库工厂配置类，用于配置数据库连接信息
/// </summary>
public class FactoryConfig
{
    /// <summary>
    /// 数据库类型，指定要使用的数据库管理系统类型
    /// </summary>
    public string DbType { get; set; } = string.Empty;

    /// <summary>
    /// 主数据库连接字符串，用于配置主数据库的连接信息
    /// </summary>
    public string Master { get; set; } = string.Empty;

    /// <summary>
    /// 数据库名称，用于标识数据库实例
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 从数据库连接字符串集合，用于配置读写分离时的从数据库连接信息
    /// </summary>
    public List<string> Slaves { get; set; } = [];
}
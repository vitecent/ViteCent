namespace ViteCent.Core.Orm;

/// <summary>
/// 数据库定义类，用于描述数据库的整体参数信息
/// </summary>
public class BaseDatabaseInfo
{
    /// <summary>
    /// 数据表集合，包含该数据库中所有表的定义信息
    /// </summary>
    public List<BaseTableInfo> BaseTables { get; set; } = [];

    /// <summary>
    /// 数据库名称，对应实际的数据库名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    public string CharSet { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 简介
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 服务器
    /// </summary>
    public string Server { get; set; } = string.Empty;

    /// <summary>
    /// 类型
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string User { get; set; } = string.Empty;
}
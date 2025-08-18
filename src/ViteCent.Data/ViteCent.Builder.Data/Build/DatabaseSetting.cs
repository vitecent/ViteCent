namespace ViteCent.Builder.Data.Build;

/// <summary>
/// </summary>
public class DatabaseSetting
{
    /// <summary>
    /// </summary>
    public string CamelCaseName { get; set; } = string.Empty;

    /// <summary>
    /// 编码
    /// </summary>
    public string CharSet { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Password { get; set; } = "123456";

    /// <summary>
    /// </summary>
    public string Port { get; set; } = "3306";

    /// <summary>
    /// </summary>
    public string Server { get; set; } = "192.168.0.8";

    /// <summary>
    /// </summary>
    public List<Table> Tables { get; set; } = [];

    /// <summary>
    /// </summary>
    public string Type { get; set; } = "MySql";

    /// <summary>
    /// </summary>
    public string User { get; set; } = "root";
}
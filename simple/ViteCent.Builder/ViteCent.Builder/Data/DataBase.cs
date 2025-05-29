using ViteCent.Core.Orm;

namespace ViteCent.Builder.Data;

public class DataBase : BaseDataBase
{
    /// <summary>
    /// </summary>
    public bool Invoke { get; set; }

    /// <summary>
    /// </summary>
    public string InvokeProjrect { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string InvokeService { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Password { get; set; } = "123456";

    /// <summary>
    /// </summary>
    public string Port { get; set; } = "3306";

    /// <summary>
    /// </summary>
    public string Server { get; set; } = "192.168.0.9";

    /// <summary>
    /// </summary>
    public string ServiceAddress { get; set; } = "192.168.0.53";

    /// <summary>
    /// </summary>
    public string ServiceName { get; set; } = "Service";

    /// <summary>
    /// </summary>
    public int ServicePort { get; set; }

    /// <summary>
    /// </summary>
    public string SmallName { get; set; } = string.Empty;

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
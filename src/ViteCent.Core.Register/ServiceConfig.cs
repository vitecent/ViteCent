namespace ViteCent.Core.Register;

/// <summary>
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Check { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Deregister { get; set; }

    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public bool IsHttps { get; set; }

    /// <summary>
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// </summary>
    public int Timeout { get; set; }
}
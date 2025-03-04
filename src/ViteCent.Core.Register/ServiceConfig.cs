namespace ViteCent.Core.Register;

/// <summary>
/// </summary>
public class ServiceConfig
{
    /// <summary>
    /// </summary>
    /// <value>The address.</value>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The check.</value>
    public string Check { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The deregister.</value>
    public int Deregister { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The identifier.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The port.</value>
    public bool IsHttps { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The port.</value>
    public int Port { get; set; }

    /// <summary>
    /// </summary>
    /// <value>The timeout.</value>
    public int Timeout { get; set; }
}
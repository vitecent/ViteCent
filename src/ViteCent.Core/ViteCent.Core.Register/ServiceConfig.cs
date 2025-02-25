namespace ViteCent.Core.Register;

/// <summary>
///     Class ServiceConfig.
/// </summary>
public class ServiceConfig
{
    /// <summary>
    ///     Gets or sets the address.
    /// </summary>
    /// <value>The address.</value>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the check.
    /// </summary>
    /// <value>The check.</value>
    public string Check { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the deregister.
    /// </summary>
    /// <value>The deregister.</value>
    public int Deregister { get; set; }

    /// <summary>
    ///     Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the port.
    /// </summary>
    /// <value>The port.</value>
    public bool IsHttps { get; set; }

    /// <summary>
    ///     Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets the port.
    /// </summary>
    /// <value>The port.</value>
    public int Port { get; set; }

    /// <summary>
    ///     Gets or sets the timeout.
    /// </summary>
    /// <value>The timeout.</value>
    public int Timeout { get; set; }
}
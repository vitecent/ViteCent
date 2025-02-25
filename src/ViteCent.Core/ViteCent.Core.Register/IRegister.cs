namespace ViteCent.Core.Register;

/// <summary>
///     Interface IRegister
/// </summary>
public interface IRegister
{
    /// <summary>
    ///     Deregisters the asynchronous.
    /// </summary>
    /// <param name="serviceId">The service identifier.</param>
    /// <returns>Task.</returns>
    Task DeregisterAsync(string serviceId);

    /// <summary>
    ///     Discovers the asynchronous.
    /// </summary>
    /// <returns>Task&lt;Dictionary&lt;System.String, List&lt;ServiceConfig&gt;&gt;&gt;.</returns>
    Task<Dictionary<string, List<ServiceConfig>>> DiscoverAsync();

    /// <summary>
    ///     Registers the asynchronous.
    /// </summary>
    /// <param name="microService">The micro service.</param>
    /// <returns>Task.</returns>
    Task RegisterAsync(ServiceConfig microService);
}
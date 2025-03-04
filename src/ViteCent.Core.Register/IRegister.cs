namespace ViteCent.Core.Register;

/// <summary>
/// </summary>
public interface IRegister
{
    /// <summary>
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
    Task DeregisterAsync(string serviceId);

    /// <summary>
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<string, List<ServiceConfig>>> DiscoverAsync();

    /// <summary>
    /// </summary>
    /// <param name="microService"></param>
    /// <returns></returns>
    Task RegisterAsync(ServiceConfig microService);
}
#region

using Consul;

#endregion

namespace ViteCent.Core.Register.Consul;

/// <summary>
///     Class ConsulRegister. Implements the <see cref="ViteCent.Core.Register.IRegister" />
/// </summary>
/// <seealso cref="ViteCent.Core.Register.IRegister" />
/// <param name="uri"></param>
public class ConsulRegister(string uri) : IRegister
{
    /// <summary>
    ///     The client
    /// </summary>
    private readonly ConsulClient client = new(x => { x.Address = new Uri(uri); });

    /// <summary>
    ///     Deregisters the asynchronous.
    /// </summary>
    /// <param name="serviceId">The service identifier.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task DeregisterAsync(string serviceId)
    {
        await client.Agent.ServiceDeregister(serviceId);
    }

    /// <summary>
    ///     Discovers the asynchronous.
    /// </summary>
    /// <returns>A Task&lt;Dictionary`2&gt; representing the asynchronous operation.</returns>
    public async Task<Dictionary<string, List<ServiceConfig>>> DiscoverAsync()
    {
        var result = new Dictionary<string, List<ServiceConfig>>();

        var services = await client.Agent.Services();

        foreach (var service in services.Response)
        {
            var item = service.Value;

            if (result.TryGetValue(item.Service.ToLower(), out var list))
            {
                list.Add(new ServiceConfig
                {
                    Id = service.Key,
                    Name = item.Service,
                    Address = item.Address,
                    Port = item.Port
                });
            }
            else
            {
                list =
                [
                    new ServiceConfig
                    {
                        Id = service.Key,
                        Name = item.Service,
                        Address = item.Address,
                        Port = item.Port
                    }
                ];

                result.Add(item.Service.ToLower(), list);
            }
        }

        return result;
    }

    /// <summary>
    ///     Register as an asynchronous operation.
    /// </summary>
    /// <param name="microService">The micro service.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task RegisterAsync(ServiceConfig microService)
    {
        var service = new AgentServiceRegistration
        {
            ID = microService.Id,
            Name = microService.Name,
            Address = microService.Address,
            Port = microService.Port,
            Check = new AgentServiceCheck
            {
                Interval = TimeSpan.FromSeconds(30),
                HTTP = $"http://{microService.Address}:{microService.Port}{microService.Check}",
                Timeout = TimeSpan.FromSeconds(microService.Timeout),
                DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(microService.Deregister)
            }
        };

        if (microService.IsHttps) service.Check.HTTP = service.Check.HTTP.Replace("http://", "https://");

        await client.Agent.ServiceRegister(service);
    }
}
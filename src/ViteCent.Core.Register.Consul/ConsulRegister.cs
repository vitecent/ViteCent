#region

using Consul;

#endregion

namespace ViteCent.Core.Register.Consul;

/// <summary>
/// </summary>
/// <param name="uri"></param>
public class ConsulRegister(string uri) : IRegister
{
    /// <summary>
    /// </summary>
    private readonly ConsulClient client = new(x => { x.Address = new Uri(uri); });

    /// <summary>
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
    public async Task DeregisterAsync(string serviceId)
    {
        await client.Agent.ServiceDeregister(serviceId);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, List<ServiceConfig>>> ServiceAsync()
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
    /// </summary>
    /// <param name="microService"></param>
    /// <returns></returns>
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
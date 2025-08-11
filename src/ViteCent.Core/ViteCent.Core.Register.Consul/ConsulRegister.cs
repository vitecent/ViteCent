#region

using Consul;

#endregion

namespace ViteCent.Core.Register.Consul;

/// <summary>
/// Consul服务注册实现类，提供服务注册、注销和服务查询功能
/// </summary>
/// <param name="uri">Consul服务器地址</param>
public class ConsulRegister(string uri) : IRegister
{
    /// <summary>
    /// Consul客户端实例，用于与Consul服务器进行通信
    /// </summary>
    private readonly ConsulClient client = new(x => { x.Address = new Uri(uri); });

    /// <summary>
    /// 注销指定的服务实例
    /// </summary>
    /// <param name="serviceId">要注销的服务实例标识</param>
    /// <returns>任务</returns>
    public async Task DeregisterAsync(string serviceId)
    {
        await client.Agent.ServiceDeregister(serviceId);
    }

    /// <summary>
    /// 注册微服务实例到Consul
    /// </summary>
    /// <param name="microService">微服务配置信息，包含服务标识、名称、地址、端口等</param>
    /// <returns>任务</returns>
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

    /// <summary>
    /// 获取当前注册的所有服务实例信息
    /// </summary>
    /// <returns>返回服务名称与服务实例列表的字典集合</returns>
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
}
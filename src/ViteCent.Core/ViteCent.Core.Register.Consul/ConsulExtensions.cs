#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Register.Consul;

/// <summary>
/// Consul服务注册配置扩展类，提供Consul服务注册和配置的功能
/// </summary>
public static class ConsulExtensions
{
    /// <summary>
    /// 添加Consul服务注册到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息，用于获取Consul服务地址</param>
    /// <returns>配置后的服务集合</returns>
    public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
    {
        var uri = configuration["Register"] ?? default!;

        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Register");

        services.AddTransient<IRegister>(x => new ConsulRegister(uri));

        return services;
    }

    /// <summary>
    /// 启用Consul服务注册，将当前服务注册到Consul服务中心
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>配置后的应用程序构建器</returns>
    public static async Task<IApplicationBuilder> UseConsulAsync(this WebApplication app)
    {
        var logger = new BaseLogger(typeof(ConsulExtensions));

        var configuration = app.Configuration;

        var uri = configuration["Register"] ?? default!;

        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Register");

        logger.LogInformation($"Consul RegisterUri ：{uri}");

        var serviceName = configuration["Service:Name"] ?? default!;

        logger.LogInformation($"Consul ServiceName ：{serviceName}");

        if (string.IsNullOrWhiteSpace(serviceName)) throw new Exception("Appsettings Must Be ServiceConfig.Name");

        var isDapr = configuration["Environment"] ?? default!;

        logger.LogInformation($"Consul IsDapr ：{isDapr}");

        var configPoint = configuration["Port"] ?? default!;

        if (isDapr != "Dapr") configPoint = configuration["Service:Port"] ?? default!;

        logger.LogInformation($"Consul ServicePoint ：{configPoint}");

        var flagServicePort = int.TryParse(configPoint, out var servicePort);

        if (!flagServicePort || servicePort < 1) throw new Exception("Appsettings Must Be ServiceConfig.Port");

        var address = configuration["Service:Address"] ?? default!;

        logger.LogInformation($"Consul ServiceAddress ：{address}");

        if (string.IsNullOrWhiteSpace(address)) throw new Exception("Appsettings Must Be ServiceConfig.Address");

        var serviceId = configuration["Service:Id"] ?? default!;

        if (string.IsNullOrWhiteSpace(serviceId)) serviceId = $"{serviceName}:{address}:{servicePort}";

        logger.LogInformation($"Consul ServiceId ：{serviceId}");

        var flagTimeout = int.TryParse(configuration["Service:Timeout"] ?? default!, out var timeout);

        if (!flagTimeout || timeout < 1) timeout = 5;

        logger.LogInformation($"Consul ServiceTimeout ：{timeout}");

        var flagDeregister = int.TryParse(configuration["Service:Deregister"] ?? default!, out var deregister);

        if (!flagDeregister || deregister < 1) deregister = 30;

        logger.LogInformation($"Consul ServiceDeregister ：{deregister}");

        var isHttps = bool.TryParse(configuration["Service:Https"] ?? default!, out var iShttps);

        logger.LogInformation($"Consul ServiceHttps ：{isHttps}");

        var check = configuration["Service:Check"] ?? default!;

        if (string.IsNullOrWhiteSpace(check)) check = BaseConst.Check;

        logger.LogInformation($"Consul ServiceCheck ：{check}");

        var service = new ServiceConfig
        {
            Id = serviceId,
            Name = serviceName,
            Port = servicePort,
            Address = address,
            Timeout = timeout,
            Deregister = deregister,
            IsHttps = iShttps,
            Check = check
        };

        await new ConsulRegister(uri).RegisterAsync(service);

        if (check == BaseConst.Check) app.MapGet(check, () => new BaseResult());

        var lifetime = app.Lifetime;

        lifetime.ApplicationStopping.Register(async () =>
        {
            await new ConsulRegister(uri).DeregisterAsync(serviceId);
        });

        return app;
    }
}
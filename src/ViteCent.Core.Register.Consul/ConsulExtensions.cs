#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Register.Consul;

/// <summary>
///     Class ConsulExtensions.
/// </summary>
public static class ConsulExtensions
{
    /// <summary>
    ///     Adds the consul.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddConsul(this IServiceCollection services, IConfiguration configuration)
    {
        var uri = configuration["Register"] ?? default!;

        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Register");

        services.AddTransient<IRegister>(x => new ConsulRegister(uri));

        return services;
    }

    /// <summary>
    ///     Uses the consul.
    /// </summary>
    /// <param name="app">The app.</param>
    /// <returns>IApplicationBuilder.</returns>
    public static async Task<IApplicationBuilder> UseConsulAsync(this WebApplication app)
    {
        var logger = BaseLogger.GetLogger();

        var configuration = app.Configuration;

        var uri = configuration["Register"] ?? default!;

        logger.Info($"Consul RegisterUri ：{uri}");

        if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Register");

        var serviceName = configuration["Service:Name"] ?? default!;

        logger.Info($"Consul ServiceName ：{serviceName}");

        if (string.IsNullOrWhiteSpace(serviceName)) throw new Exception("Appsettings Must Be ServiceConfig.Name");

        var isDapr = configuration["Environment"] ?? default!;

        logger.Info($"Consul IsDapr ：{isDapr}");

        var configPoint = configuration["Port"] ?? default!;

        if (isDapr != "Dapr") configPoint = configuration["Service:Port"] ?? default!;

        logger.Info($"Consul ServicePoint ：{configPoint}");

        var flagServicePort = int.TryParse(configPoint, out var servicePort);

        if (!flagServicePort || servicePort < 1) throw new Exception("Appsettings Must Be ServiceConfig.Port");

        var serviceId = configuration["Service:Id"] ?? default!;

        if (string.IsNullOrWhiteSpace(serviceId)) serviceId = $"{serviceName}:{servicePort}";

        logger.Info($"Consul ServiceId ：{serviceId}");

        var address = configuration["Service:Address"] ?? default!;

        logger.Info($"Consul ServiceAddress ：{address}");

        if (string.IsNullOrWhiteSpace(address)) throw new Exception("Appsettings Must Be ServiceConfig.Address");

        var flagTimeout = int.TryParse(configuration["Service:Timeout"] ?? default!, out var timeout);

        if (!flagTimeout || timeout < 1) timeout = 5;

        logger.Info($"Consul ServiceTimeout ：{timeout}");

        var flagDeregister = int.TryParse(configuration["Service:Deregister"] ?? default!, out var deregister);

        if (!flagDeregister || deregister < 1) deregister = 30;

        logger.Info($"Consul ServiceDeregister ：{deregister}");

        var isHttps = bool.TryParse(configuration["Service:Https"] ?? default!, out var iShttps);

        logger.Info($"Consul ServiceHttps ：{isHttps}");

        var check = configuration["Service:Check"] ?? default!;

        if (string.IsNullOrWhiteSpace(check)) check = Const.Check;

        logger.Info($"Consul ServiceCheck ：{check}");

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

        if (check == Const.Check) app.MapGet(check, () => new BaseResult());

        var lifetime = app.Lifetime;

        lifetime.ApplicationStopping.Register(async () =>
        {
            await new ConsulRegister(uri).DeregisterAsync(serviceId);
        });

        return app;
    }
}
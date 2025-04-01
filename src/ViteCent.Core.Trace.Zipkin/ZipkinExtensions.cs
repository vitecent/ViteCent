#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Trace.Zipkin;

/// <summary>
/// </summary>
public static class ZipkinExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddZipkin(this IServiceCollection services, IConfiguration configuration)
    {
        var isDapr = configuration["Environment"] ?? default!;

        if (isDapr != "Dapr")
        {
            var logger = new BaseLogger(typeof(ZipkinExtensions));

            var serviceName = configuration["Service:Name"] ?? default!;

            logger.LogInformation($"Consul ServiceName ：{serviceName}");

            if (string.IsNullOrWhiteSpace(serviceName)) throw new Exception("Appsettings Must Be ServiceConfig.Name");

            var uri = configuration["Trace"];

            if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Trace");

            logger.LogInformation($"Zipkin RegisterUri ：{uri}");

            var check = configuration["Service:Check"];

            if (string.IsNullOrWhiteSpace(check)) check = Const.Check;

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder.AddSource(serviceName)
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                        .AddAspNetCoreInstrumentation(option =>
                        {
                            option.Filter = (httpContext) => !httpContext.Request.Path.StartsWithSegments(check);
                        })
                        .AddHttpClientInstrumentation()
                        .AddZipkinExporter(zipkin =>
                        {
                            zipkin.Endpoint = new Uri(uri);
                        });
                });
        }

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseZipkin(this WebApplication app)
    {
        return app;
    }
}
#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Trace.Zipkin;

/// <summary>
/// Zipkin链路追踪配置工具类，提供配置和启用Zipkin服务的功能
/// </summary>
public static class ZipkinExtensions
{
    /// <summary>
    /// 添加Zipkin链路追踪服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息</param>
    /// <returns>配置后的服务集合</returns>
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

            if (string.IsNullOrWhiteSpace(check)) check = BaseConst.Check;

            services.AddOpenTelemetry()
                .WithTracing(builder =>
                {
                    builder.AddSource(serviceName)
                        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName))
                        .AddAspNetCoreInstrumentation(option =>
                        {
                            option.Filter = httpContext => !httpContext.Request.Path.StartsWithSegments(check);
                        })
                        .AddHttpClientInstrumentation()
                        .AddZipkinExporter(zipkin => { zipkin.Endpoint = new Uri(uri); });
                });
        }

        return services;
    }

    /// <summary>
    /// 启用Zipkin链路追踪中间件
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>配置后的应用程序构建器</returns>
    public static IApplicationBuilder UseZipkin(this WebApplication app)
    {
        return app;
    }
}
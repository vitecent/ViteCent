#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ViteCent.Core.Data;
using zipkin4net;
using zipkin4net.Middleware;
using zipkin4net.Tracers.Zipkin;
using zipkin4net.Transport.Http;

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
            var logger = BaseLogger.GetLogger(typeof(ZipkinExtensions));

            TraceManager.SamplingRate = 1.0f;

            var loggerFactory = new LoggerFactory();
            var log = new TracingLogger(loggerFactory, "ZipkinExtensions");

            var uri = configuration["Trace"];

            logger.Info($"Zipkin RegisterUri ：{uri}");

            if (string.IsNullOrWhiteSpace(uri)) throw new Exception("Appsettings Must Be Trace");

            var httpSender = new HttpZipkinSender(uri, "application/json");
            var tracer = new ZipkinTracer(httpSender, new JSONSpanSerializer());

            TraceManager.RegisterTracer(tracer);
            TraceManager.Start(log);
        }

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseZipkin(this WebApplication app)
    {
        var configuration = app.Configuration;

        var isDapr = configuration["Environment"] ?? default!;

        if (isDapr != "Dapr")
        {
            var serviceName = configuration["Service:Name"];

            var check = configuration["Service:Check"];

            if (string.IsNullOrWhiteSpace(check)) check = Const.Check;

            app.UseTracing(serviceName, null, x => { return x != check; });

            var lifetime = app.Lifetime;

            lifetime.ApplicationStopping.Register(() => { TraceManager.Stop(); });
        }

        return app;
    }
}
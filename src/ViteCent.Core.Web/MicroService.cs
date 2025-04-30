#region

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using System.IO.Compression;
using ViteCent.Core.Logging.Log4Net;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public abstract class MicroService
{
    private readonly BaseLogger logger;

    /// <summary>
    /// </summary>
    protected MicroService()
    {
        logger = new BaseLogger(typeof(MicroService));

        logger.LogInformation("开始初始化微服务");
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public virtual async Task RunAsync(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddEnvironmentVariables();
        var configuration = builder.Configuration;
        configuration.SetBasePath(Directory.GetCurrentDirectory());

        await ConfigAsync(configuration);

        var isDapr = configuration["Environment"] ?? default!;

        var configPoint = configuration["Port"] ?? default!;

        if (isDapr != "Dapr") configPoint = configuration["Service:Port"] ?? default!;

        logger.LogInformation($"Host ServicePoint ：{configPoint}");

        var flagServicePort = int.TryParse(configPoint, out var servicePort);

        if (!flagServicePort || servicePort < 1) throw new Exception("Appsettings Must Be ServiceConfig.Port");

        builder.WebHost.UseUrls($"http://*:{servicePort}");

        var services = builder.Services;

        services.AddLog4Net();
        services.AddHttpContextAccessor();

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.Configure<BrotliCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });

        services.AddControllers(options => options.Filters.Add(new BaseExceptionFilter()))
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.None;
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.Converters =
                [
                    new IsoDateTimeConverter
                    {
                        DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                    }
                ];
            }).AddDapr();

        await BuildAsync(builder);

        var app = builder.Build();

        app.UseResponseCompression();

        await StartAsync(app);

        var lifetime = app.Lifetime;

        lifetime.ApplicationStopping.Register(async () => { await StopAsync(); });

        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected virtual async Task BuildAsync(WebApplicationBuilder builder)
    {
        logger.LogInformation("开始构建微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    protected virtual async Task ConfigAsync(IConfiguration configuration)
    {
        logger.LogInformation("开始配置微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    protected virtual async Task StartAsync(WebApplication app)
    {
        logger.LogInformation("开始启动微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected virtual async Task StopAsync()
    {
        logger.LogInformation("开始停止微服务");
        await Task.CompletedTask;
    }
}
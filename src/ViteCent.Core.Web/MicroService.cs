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
/// 微服务基础抽象类，提供微服务的基本功能和生命周期管理
/// </summary>
/// <remarks>
/// 该类作为所有微服务的基类，提供统一的配置、构建、启动和停止等生命周期管理。
/// 继承此类可以快速构建一个具备标准功能的微服务应用。
/// </remarks>
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
    /// 运行微服务应用
    /// </summary>
    /// <param name="args">启动参数数组</param>
    /// <returns>异步任务</returns>
    /// <remarks>
    /// 该方法执行以下操作：
    /// 1. 初始化WebApplicationBuilder
    /// 2. 配置服务和中间件
    /// 3. 启动HTTP服务器
    /// 4. 注册生命周期事件
    /// </remarks>
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
    /// 构建微服务应用
    /// </summary>
    /// <param name="builder">Web应用构建器</param>
    /// <returns>异步任务</returns>
    /// <remarks>
    /// 在此方法中可以添加自定义服务注册、配置中间件等构建操作
    /// </remarks>
    protected virtual async Task BuildAsync(WebApplicationBuilder builder)
    {
        logger.LogInformation("开始构建微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// 配置微服务应用
    /// </summary>
    /// <param name="configuration">配置接口</param>
    /// <returns>异步任务</returns>
    /// <remarks>
    /// 在此方法中可以进行配置文件加载、环境变量设置等配置操作
    /// </remarks>
    protected virtual async Task ConfigAsync(IConfiguration configuration)
    {
        logger.LogInformation("开始配置微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// 启动微服务应用
    /// </summary>
    /// <param name="app">Web应用实例</param>
    /// <returns>异步任务</returns>
    /// <remarks>
    /// 在此方法中可以进行服务启动前的准备工作，如初始化资源、启动后台任务等
    /// </remarks>
    protected virtual async Task StartAsync(WebApplication app)
    {
        logger.LogInformation("开始启动微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// 停止微服务应用
    /// </summary>
    /// <returns>异步任务</returns>
    /// <remarks>
    /// 在此方法中可以进行服务停止时的清理工作，如释放资源、保存状态等
    /// </remarks>
    protected virtual async Task StopAsync()
    {
        logger.LogInformation("开始停止微服务");
        await Task.CompletedTask;
    }
}
#region

using log4net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

using System.IO.Compression;
using ViteCent.Core.Authorize.Jwt;
using ViteCent.Core.Data;
using ViteCent.Core.Logging.Log4Net;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
///     微服务基类，提供启动、配置、构建和停止的抽象方法
/// </summary>
public abstract class MicroService
{
    private readonly ILog logger;

    /// <summary>
    /// </summary>
    protected MicroService()
    {
        logger = BaseLogger.GetLogger();

        logger.Info("开始初始化微 服务");
    }

    /// <summary>
    ///     运行微服务
    /// </summary>
    /// <param name="args">启动参数</param>
    /// <returns>异步任务</returns>
    public virtual async Task RunAsync(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;
        configuration.SetBasePath(Directory.GetCurrentDirectory());

        await ConfigAsync(configuration);

        //Builder
        var services = builder.Services;

        services.AddLog4Net();

        services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
        BaseHttpContext.Services = services;

        services.AddResponseCompression();
        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Fastest; });
        services.AddEndpointsApiExplorer();

        services.AddControllers(options => { options.Filters.Add(new BaseExceptionFilter()); }).AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }).AddDapr();

        // 添加Jwt服务
        logger.Info("开始添加 Jwt 服务");
        services.AddJwt(configuration);

        await BuildAsync(builder);

        //App
        var app = builder.Build();

        await StartAsync(app);

        var lifetime = app.Lifetime;

        lifetime.ApplicationStopping.Register(async () => { await StopAsync(); });

        if (app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/simple/error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // 使用 Jwt 中间件
        logger.Info("开始使用 Jwt 中间件");
        app.UseJwt();

        app.MapControllers();

        app.Run();
    }

    /// <summary>
    ///     构建微服务
    /// </summary>
    /// <param name="builder">Web 应用程序构建器</param>
    /// <returns>异步任务</returns>
    protected virtual async Task BuildAsync(WebApplicationBuilder builder)
    {
        logger.Info("开始构建微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    ///     配置微服务
    /// </summary>
    /// <param name="configuration">配置对象</param>
    /// <returns>异步任务</returns>
    protected virtual async Task ConfigAsync(IConfiguration configuration)
    {
        logger.Info("开始配置微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    ///     启动微服务
    /// </summary>
    /// <param name="app">Web 应用程序</param>
    /// <returns>异步任务</returns>
    protected virtual async Task StartAsync(WebApplication app)
    {
        logger.Info("开始启动微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    ///     停止微服务
    /// </summary>
    /// <returns>异步任务</returns>
    protected virtual async Task StopAsync()
    {
        logger.Info("开始停止微服务");
        await Task.CompletedTask;
    }
}
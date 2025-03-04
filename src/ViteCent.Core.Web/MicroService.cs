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
/// </summary>
public abstract class MicroService
{
    private readonly ILog logger;

    /// <summary>
    /// </summary>
    protected MicroService()
    {
        logger = BaseLogger.GetLogger();

        logger.Info("开始初始化微服务");
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected virtual async Task BuildAsync(WebApplicationBuilder builder)
    {
        logger.Info("开始构建微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    protected virtual async Task ConfigAsync(IConfiguration configuration)
    {
        logger.Info("开始配置微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    protected virtual async Task StartAsync(WebApplication app)
    {
        logger.Info("开始启动微服务");
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected virtual async Task StopAsync()
    {
        logger.Info("开始停止微服务");
        await Task.CompletedTask;
    }
}
#region

using log4net;
using Microsoft.AspNetCore.Builder;
using ViteCent.Core.Api.Swagger;
using ViteCent.Core.Cache.Redis;
using ViteCent.Core.Register.Consul;
using ViteCent.Core.Trace.Zipkin;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public class FilesMicroService : MicroService
{
    /// <summary>
    /// </summary>
    private readonly ILog logger;

    /// <summary>
    /// </summary>
    private readonly string title;

    /// <summary>
    /// </summary>
    private readonly List<string> xmls;

    /// <summary>
    /// </summary>
    /// <param name="title"></param>
    /// <param name="xmls"></param>
    public FilesMicroService(string title, List<string> xmls)
    {
        this.title = title;
        this.xmls = xmls;

        logger = BaseLogger.GetLogger();

        logger.Info("开始构建文档微服务");
    }

    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        // 调用基类的 BuildAsync 方法
        await base.BuildAsync(builder);

        var configuration = builder.Configuration;
        var services = builder.Services;

        // 添加 Redis 缓存服务
        logger.Info("开始添加  Redis 缓存服务");
        services.AddRedis(configuration);

        // 添加 Consul 注册服务
        logger.Info("开始添加  Consul 注册服务");
        services.AddConsul(configuration);

        // 添加 Zipkin 链路追踪服务
        logger.Info("开始添加  Zipkin 链路追踪服务");
        services.AddZipkin(configuration);

        // 添加 Swagger 文档服务
        logger.Info("开始添加  Swagger 文档服务");
        services.AddSwagger(title, xmls);
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    protected override async Task StartAsync(WebApplication app)
    {
        // 调用基类的 StartAsync 方法
        await base.StartAsync(app);

        // 使用 Consul 注册中间件
        logger.Info("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        // 使用 Zipkin 链路追踪中间件
        logger.Info("开始使用 Zipkin 中间件");
        app.UseZipkin();

        // 映射静态资源
        logger.Info("开始映射静态资源");
        app.MapStaticAssets();

        // 使用 Swagger 仪表板
        logger.Info("开始使用 Swagger 仪表板");
        app.UseSwagger();
    }
}
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

        logger = BaseLogger.GetLogger(typeof(FilesMicroService));

        logger.Info("开始构建文档微服务");
    }

    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        await base.BuildAsync(builder);

        var configuration = builder.Configuration;
        var services = builder.Services;

        logger.Info("开始添加  Redis 缓存服务");
        services.AddRedis(configuration);

        logger.Info("开始添加  Consul 注册服务");
        services.AddConsul(configuration);

        logger.Info("开始添加  Zipkin 链路追踪服务");
        services.AddZipkin(configuration);

        logger.Info("开始添加  Swagger 文档服务");
        services.AddSwagger(title, xmls);
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    protected override async Task StartAsync(WebApplication app)
    {
        await base.StartAsync(app);

        logger.Info("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        logger.Info("开始使用 Zipkin 中间件");
        app.UseZipkin();

        logger.Info("开始映射静态资源");
        app.MapStaticAssets();

        logger.Info("开始使用 Swagger 仪表板");
        app.UseSwagger();
    }
}
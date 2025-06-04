#region

using Microsoft.AspNetCore.Builder;
using ViteCent.Core.Api.Swagger;
using ViteCent.Core.Register.Consul;
using ViteCent.Core.Trace.Zipkin;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 文档微服务类，提供Consul服务注册、Zipkin链路追踪和Swagger文档服务等功能
/// </summary>
public class FilesMicroService : MicroService
{
    /// <summary>
    /// 日志记录器实例
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// Swagger文档标题
    /// </summary>
    private readonly string title;

    /// <summary>
    /// XML文档文件路径列表
    /// </summary>
    private readonly List<string> xmls;

    /// <summary>
    /// 初始化文档微服务实例
    /// </summary>
    /// <param name="title">Swagger文档标题</param>
    /// <param name="xmls">XML文档文件路径列表</param>
    public FilesMicroService(string title, List<string> xmls)
    {
        this.title = title;
        this.xmls = xmls;

        logger = new BaseLogger(typeof(FilesMicroService));

        logger.LogInformation("开始构建文档微服务");
    }

    /// <summary>
    /// 构建微服务，配置Consul注册、Zipkin链路追踪和Swagger文档服务
    /// </summary>
    /// <param name="builder">Web应用程序构建器</param>
    /// <returns>任务</returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        await base.BuildAsync(builder);

        var configuration = builder.Configuration;
        var services = builder.Services;

        logger.LogInformation("开始添加  Consul 注册服务");
        services.AddConsul(configuration);

        logger.LogInformation("开始添加  Zipkin 链路追踪服务");
        services.AddZipkin(configuration);

        logger.LogInformation("开始添加  Swagger 文档服务");
        services.AddSwagger(title, xmls);
    }

    /// <summary>
    /// 启动微服务，配置中间件管道
    /// </summary>
    /// <param name="app">Web应用程序实例</param>
    /// <returns>任务</returns>
    protected override async Task StartAsync(WebApplication app)
    {
        await base.StartAsync(app);

        logger.LogInformation("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        logger.LogInformation("开始使用 Zipkin 中间件");
        app.UseZipkin();

        logger.LogInformation("开始映射静态资源");
        app.MapStaticAssets();

        logger.LogInformation("开始使用 Swagger 仪表板");
        app.UseSwagger();
    }
}
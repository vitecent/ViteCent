#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Api.Swagger;
using ViteCent.Core.Authorize.Jwt;
using ViteCent.Core.Cache.Redis;
using ViteCent.Core.Orm.SqlSugar;
using ViteCent.Core.Register.Consul;
using ViteCent.Core.Trace.Zipkin;
using ViteCent.Core.Web.Filter;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 基础微服务类，提供统一的服务配置和中间件集成功能
/// </summary>
public class BaseMicroService : MicroService
{
    /// <summary>
    /// 日志记录器实例
    /// </summary>
    private readonly BaseLogger logger;

    /// <summary>
    /// 服务标题
    /// </summary>
    private readonly string title;

    /// <summary>
    /// XML文档路径列表
    /// </summary>
    private readonly List<string> xmls;

    /// <summary>
    /// 初始化基础微服务实例
    /// </summary>
    /// <param name="title">服务的标题，用于Swagger文档显示</param>
    /// <param name="xmls">XML文档路径列表，用于Swagger文档生成</param>
    public BaseMicroService(string title, List<string> xmls)
    {
        this.title = title;
        this.xmls = xmls;

        logger = new BaseLogger(typeof(BaseMicroService));

        logger.LogInformation("开始构建基础微服务");
    }

    /// <summary>
    /// 服务构建回调委托，用于在服务构建阶段添加自定义配置
    /// </summary>
    public Action<WebApplicationBuilder> OnBuild { get; set; } = default!;

    /// <summary>
    /// 服务启动回调委托，用于在服务启动阶段添加自定义中间件
    /// </summary>
    public Action<WebApplication> OnStart { get; set; } = default!;

    /// <summary>
    /// 构建微服务应用，配置基础服务和中间件
    /// </summary>
    /// <param name="builder">Web应用构建器实例</param>
    /// <returns>任务</returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        await base.BuildAsync(builder);

        builder.Services.AddTransient<BaseLoginFilter>();
        builder.Services.AddTransient<BaseAuthFilter>();

        builder.Services.AddTransient(typeof(IBaseInvoke<,>), typeof(BaseInvoke<,>));

        var configuration = builder.Configuration;
        var services = builder.Services;

        logger.LogInformation("开始添加 Redis 服务");
        services.AddRedis(configuration);

        logger.LogInformation("开始添加 Consul 服务");
        services.AddConsul(configuration);

        logger.LogInformation("开始添加 Zipkin 服务");
        services.AddZipkin(configuration);

        logger.LogInformation("开始添加 SqlSugar 服务");
        services.AddSqlSugger(configuration);

        logger.LogInformation("开始添加 Swagger 服务");
        services.AddSwagger(title, xmls);

        logger.LogInformation("开始添加 Jwt 服务");
        services.AddJwt(configuration);

        logger.LogInformation("开始执行构建回调");
        OnBuild?.Invoke(builder);
    }

    /// <summary>
    /// 启动微服务应用，配置中间件管道
    /// </summary>
    /// <param name="app">Web应用实例</param>
    /// <returns>任务</returns>
    protected override async Task StartAsync(WebApplication app)
    {
        await base.StartAsync(app);

        logger.LogInformation("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        logger.LogInformation("开始使用 Zipkin 中间件");
        app.UseZipkin();

        logger.LogInformation("开始使用 Swagger 仪表盘");
        app.UseSwagger();

        logger.LogInformation("开始使用 Jwt 中间件");
        app.UseJwt();

        logger.LogInformation("开执行启动回调");
        OnStart?.Invoke(app);
    }
}
#region

using Microsoft.AspNetCore.Builder;
using Quartz;
using ViteCent.Core.Api.Swagger;
using ViteCent.Core.Cache.Redis;
using ViteCent.Core.Job.Quartz;
using ViteCent.Core.Orm.SqlSugar;
using ViteCent.Core.Register.Consul;
using ViteCent.Core.Trace.Zipkin;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 任务调度微服务类，提供Quartz任务调度、Consul服务注册、Zipkin链路追踪等功能
/// </summary>
public class JobMicroService : MicroService
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
    /// Quartz调度器实例
    /// </summary>
    private IScheduler scheduler = default!;

    /// <summary>
    /// 初始化任务调度微服务
    /// </summary>
    /// <param name="title">服务标题，用于Swagger文档显示</param>
    /// <param name="xmls">XML文档路径列表，用于Swagger文档生成</param>
    public JobMicroService(string title, List<string> xmls)
    {
        this.title = title;
        this.xmls = xmls;

        logger = new BaseLogger(typeof(JobMicroService));

        logger.LogInformation("开始构建任务微服务");
    }

    /// <summary>
    /// 构建服务时的回调委托，用于自定义服务配置
    /// </summary>
    public Action<WebApplicationBuilder> OnBuild { get; set; } = default!;

    /// <summary>
    /// 注册Quartz任务时的回调委托，用于配置任务调度
    /// </summary>
    public Action<IScheduler> OnRegist { get; set; } = default!;

    /// <summary>
    /// 构建微服务应用
    /// </summary>
    /// <param name="builder">Web应用构建器</param>
    /// <returns>构建任务</returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        await base.BuildAsync(builder);

        var configuration = builder.Configuration;
        var services = builder.Services;

        logger.LogInformation("开始添加  Redis 服务");
        services.AddRedis(configuration);

        logger.LogInformation("开始添加  Consul 服务");
        services.AddConsul(configuration);

        logger.LogInformation("开始 添加 Zipkin 服务");
        services.AddZipkin(configuration);

        logger.LogInformation("开始添加 SqlSugar 服务");
        services.AddSqlSugger(configuration);

        logger.LogInformation("开始添加  Swagger 服务");
        services.AddSwagger(title, xmls);

        logger.LogInformation("开始执行构建回调");
        OnBuild?.Invoke(builder);

        logger.LogInformation("开始初始化 Quartz 调度器");
        scheduler = await services.AddQuarzAsync();

        logger.LogInformation("开始注册 Quartz 调度器");
        var job = new Thread(() => OnRegist?.Invoke(scheduler))
        {
            IsBackground = true,
            Priority = ThreadPriority.Highest,
            Name = "Job"
        };

        job.Start();
    }

    /// <summary>
    /// 启动微服务应用
    /// </summary>
    /// <param name="app">Web应用实例</param>
    /// <returns>启动任务</returns>
    protected override async Task StartAsync(WebApplication app)
    {
        await base.StartAsync(app);

        if (scheduler is not null)
        {
            logger.LogInformation("开始使用 Quartz 调度器");
            app.UseQuarz(scheduler);
        }

        logger.LogInformation("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        logger.LogInformation("开始使用 Zipkin 中间件");
        app.UseZipkin();

        logger.LogInformation("开始使用 Swagger 仪表板");
        app.UseSwagger();
    }
}
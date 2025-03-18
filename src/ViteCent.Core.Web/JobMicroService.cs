#region

using log4net;
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
/// </summary>
public class JobMicroService : MicroService
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
    private IScheduler scheduler = default!;

    /// <summary>
    /// </summary>
    /// <param name="title"></param>
    /// <param name="xmls"></param>
    public JobMicroService(string title, List<string> xmls)
    {
        this.title = title;
        this.xmls = xmls;

        logger = BaseLogger.GetLogger();

        logger.Info("开始构建任务微服务");
    }

    /// <summary>
    /// </summary>
    public Action<WebApplicationBuilder> OnBuild { get; set; } = default!;

    /// <summary>
    /// </summary>
    public Action<IScheduler> OnRegist { get; set; } = default!;

    /// <summary>
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    protected override async Task BuildAsync(WebApplicationBuilder builder)
    {
        await base.BuildAsync(builder);

        var configuration = builder.Configuration;
        var services = builder.Services;

        logger.Info("开始添加  Redis 服务");
        services.AddRedis(configuration);

        logger.Info("开始添加  Consul 服务");
        services.AddConsul(configuration);

        logger.Info("开始 添加 Zipkin 服务");
        services.AddZipkin(configuration);

        logger.Info("开始添加  SqlSugar ORM 服务");
        services.AddSqlSugger(configuration);

        logger.Info("开始添加  Swagger 服务");
        services.AddSwagger(title, xmls);

        logger.Info("开始执行构建回调");
        OnBuild?.Invoke(builder);

        logger.Info("开始初始化 Quartz 调度器");
        scheduler = await services.AddQuarzAsync();

        logger.Info("开始注册 Quartz 调度器");
        var job = new Thread(() => OnRegist?.Invoke(scheduler))
        {
            IsBackground = true,
            Priority = ThreadPriority.Highest,
            Name = "Job"
        };

        job.Start();
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    protected override async Task StartAsync(WebApplication app)
    {
        await base.StartAsync(app);

        if (scheduler != null)
        {
            logger.Info("开始使用 Quartz 调度器");
            app.UseQuarz(scheduler);
        }

        logger.Info("开始使用 Consul 中间件");
        await app.UseConsulAsync();

        logger.Info("开始使用 Zipkin 中间件");
        app.UseZipkin();

        logger.Info("开始使用 Swagger 仪表板");
        app.UseSwagger();
    }
}
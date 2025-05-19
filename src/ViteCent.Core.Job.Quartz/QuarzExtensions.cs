#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// Quartz任务调度配置扩展类，提供Quartz服务的注册和中间件配置功能
/// </summary>
public static class QuarzExtensions
{
    /// <summary>
    /// 注册Quartz服务到依赖注入容器
    /// </summary>
    /// <param name="services">依赖注入服务集合</param>
    /// <returns>返回配置好的IScheduler实例，用于任务调度管理</returns>
    public static async Task<IScheduler> AddQuarzAsync(this IServiceCollection services)
    {
        var factory = new StdSchedulerFactory();
        var scheduler = await factory.GetScheduler();
        var provider = services.BuildServiceProvider();
        scheduler.JobFactory = new BaseJobFactory(provider);

        return scheduler;
    }

    /// <summary>
    /// 配置Quartz中间件，在应用程序启动时启动调度器，停止时关闭调度器
    /// </summary>
    /// <param name="app">Web应用程序实例</param>
    /// <param name="scheduler">Quartz调度器实例</param>
    /// <returns>返回应用程序构建器实例，支持链式调用</returns>
    public static IApplicationBuilder UseQuarz(this WebApplication app, IScheduler scheduler)
    {
        var lifetime = app.Lifetime;

        lifetime.ApplicationStarted.Register(async () => { await scheduler.Start(); });

        lifetime.ApplicationStopping.Register(async () => { await scheduler.Shutdown(); });

        return app;
    }
}
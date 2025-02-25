#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// </summary>
public static class QuarzExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static async Task<IScheduler> AddQuarzAsync(this IServiceCollection services)
    {
        var factory = new StdSchedulerFactory();
        var scheduler = await factory.GetScheduler();

        await scheduler.Start();

        return scheduler;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <param name="scheduler"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseQuarz(this WebApplication app, IScheduler scheduler)
    {
        var lifetime = app.Lifetime;

        lifetime.ApplicationStopping.Register(async () => { await scheduler.Shutdown(); });

        return app;
    }
}
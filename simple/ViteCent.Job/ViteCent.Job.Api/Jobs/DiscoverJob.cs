#region

using log4net;
using Quartz;
using ViteCent.Core;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// </summary>

/// <param name="cache"></param>
/// <param name="register"></param>
/// <param name="logger"></param>
public class DiscoverJob(IBaseCache cache, IRegister register, ILogger<DiscoverJob> logger) : IJob
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"DiscoverJob : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        var services = await register.DiscoverAsync();

        logger.LogInformation($"获取到{services.Count}个服务");

        cache.SetString(Const.RegistServices, services, TimeSpan.FromMinutes(1));
    }
}
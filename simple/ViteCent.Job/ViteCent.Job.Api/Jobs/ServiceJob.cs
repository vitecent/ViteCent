#region

using Quartz;
using ViteCent.Core.Cache;
using ViteCent.Core.Data;
using ViteCent.Core.Register;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// 服务发现作业 用于定期从注册中心获取最新的服务列表，并更新到缓存中，以便其他服务能够及时获取可用的服务信息
/// </summary>
/// <param name="cache">缓存接口，用于存储服务列表信息</param>
/// <param name="register">服务注册接口，用于从注册中心获取服务列表</param>
/// <param name="logger">日志记录器，用于记录作业执行过程中的信息</param>
public class ServiceJob(
    IBaseCache cache,
    IRegister register,
    ILogger<ServiceJob> logger)
    : IJob
{
    /// <summary>
    /// 执行服务发现任务 从注册中心获取最新的服务列表，并将其更新到缓存中，设置1分钟的过期时间
    /// </summary>
    /// <param name="context">作业执行上下文，包含作业的相关信息</param>
    /// <returns>任务</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"ServiceJob : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        var services = await register.ServiceAsync();

        logger.LogInformation($"获取到{services.Count}个服务");

        cache.SetString(BaseConst.RegistServices, services, TimeSpan.FromMinutes(1));
    }
}
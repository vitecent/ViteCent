#region

using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// Quartz作业工厂，负责创建和管理作业实例的生命周期
/// </summary>
/// <param name="provider">服务提供者，用于创建作业实例和管理依赖注入</param>
public class BaseJobFactory(IServiceProvider provider) : IJobFactory
{
    /// <summary>
    /// 创建新的作业实例
    /// </summary>
    /// <param name="bundle">触发器触发时的作业信息包，包含作业详情和触发器信息</param>
    /// <param name="scheduler">调度器实例，用于管理作业的执行</param>
    /// <returns>返回一个新的作业实例，该实例被包装在BaseJobWrapper中以管理其生命周期</returns>
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var scope = provider.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

        return new BaseJobWrapper(job ?? default!, scope);
    }

    /// <summary>
    /// 回收作业实例，释放相关资源
    /// </summary>
    /// <param name="job">需要回收的作业实例</param>
    public void ReturnJob(IJob job)
    {
        if (job is BaseJobWrapper scopedJob)
            scopedJob.Dispose();
        else
            (job as IDisposable)?.Dispose();
    }
}
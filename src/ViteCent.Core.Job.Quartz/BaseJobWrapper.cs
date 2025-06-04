#region

using Microsoft.Extensions.DependencyInjection;
using Quartz;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// Quartz作业包装器，用于管理作业执行的生命周期和资源释放
/// </summary>
/// <param name="job">需要执行的Quartz作业实例</param>
/// <param name="scope">作业执行的服务范围，用于管理依赖注入的生命周期</param>
public class BaseJobWrapper(IJob job, IServiceScope scope) : IJob, IDisposable
{
    /// <summary>
    /// 释放作业执行过程中使用的资源
    /// </summary>
    public void Dispose()
    {
        scope.Dispose();
        (job as IDisposable)?.Dispose();
    }

    /// <summary>
    /// 执行Quartz作业
    /// </summary>
    /// <param name="context">作业执行上下文，包含作业执行的相关信息</param>
    /// <returns>任务</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await job.Execute(context);
    }
}
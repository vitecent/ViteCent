#region

using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// </summary>
/// <remarks></remarks>
/// <param name="provider"></param>
public partial class BaseJobFactory(IServiceProvider provider) : IJobFactory
{
    /// <summary>
    /// </summary>
    /// <param name="bundle"></param>
    /// <param name="scheduler"></param>
    /// <returns></returns>
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        var scope = provider.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;

        return new BaseJobWrapper(job ?? default!, scope);
    }

    /// <summary>
    /// </summary>
    /// <param name="job"></param>
    public void ReturnJob(IJob job)
    {
        if (job is BaseJobWrapper scopedJob)
        {
            scopedJob.Dispose();
        }
        else
        {
            (job as IDisposable)?.Dispose();
        }
    }
}
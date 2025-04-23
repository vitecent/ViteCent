#region

using Microsoft.Extensions.DependencyInjection;
using Quartz;

#endregion

namespace ViteCent.Core.Job.Quartz;

/// <summary>
/// </summary>
/// <param name="job"></param>
/// <param name="scope"></param>
public class BaseJobWrapper(IJob job, IServiceScope scope) : IJob, IDisposable
{
    /// <summary>
    /// </summary>
    public void Dispose()
    {
        scope.Dispose();
        (job as IDisposable)?.Dispose();
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        await job.Execute(context);
    }
}
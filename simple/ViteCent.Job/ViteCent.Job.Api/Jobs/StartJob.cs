#region

using Quartz;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Orm;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// </summary>
/// <param name="logger"></param>
public class StartJob(
    ILogger<ServiceJob> logger)
    : IJob
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"StartJob : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        var urls = new List<string> {
            "http://localhost:7000/check",
            "http://localhost:8000/check",
            "http://localhost:8010/check",
            "http://localhost:8020/check",
            "http://localhost:8030/check",
            "http://localhost:8040/check",
        };

        var client = new BaseHttpClient<BaseResult>();

        foreach (var url in urls)
            try
            {
                await client.GetAsync(url);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message, e);
                continue;
            }
    }
}
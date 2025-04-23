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
            "http://localhost:7000/openapi/v1.json",
            "http://localhost:8000/openapi/v1.json",
            "http://localhost:8010/openapi/v1.json",
            "http://localhost:8020/openapi/v1.json",
            "http://localhost:8030/openapi/v1.json",
            "http://localhost:8040/openapi/v1.json",
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
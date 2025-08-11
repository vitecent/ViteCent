#region

using Quartz;
using ViteCent.Core;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// 服务启动作业 用于定期检查各个服务的健康状态，通过HTTP请求访问服务的健康检查接口
/// </summary>
/// <param name="logger">日志记录器，用于记录作业执行过程中的信息和错误</param>
public class StartJob(
    ILogger<ServiceJob> logger)
    : IJob
{
    /// <summary>
    /// 执行服务健康检查 遍历预定义的服务URL列表，向每个服务的健康检查接口发送GET请求 如果请求失败，记录错误信息并继续检查下一个服务
    /// </summary>
    /// <param name="context">作业执行上下文，包含作业的相关信息</param>
    /// <returns>任务</returns>
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
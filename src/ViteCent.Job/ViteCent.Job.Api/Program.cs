/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region

using Quartz;
using ViteCent.Core.Web;
using ViteCent.Job.Api.Jobs;

#endregion

namespace ViteCent.Job.Api;

/// <summary>
/// 作业调度服务程序入口
/// </summary>
public class Program
{
    /// <summary>
    /// 主方法 - 配置并启动作业调度服务
    /// </summary>
    /// <param name="args">启动参数</param>
    public static async Task Main(string[] args)
    {
        // 配置XML文档路径
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Job.*.xml"
        };

        // 创建作业调度微服务实例
        var microService = new JobMicroService("ViteCent.Job.Service", xmls)
        {
            // 配置服务构建
            OnBuild = builder =>
            {
                // 注册作业服务
                builder.Services.AddScoped<FingerJob>();     // 指纹采集作业
                builder.Services.AddScoped<ServiceJob>();    // 服务发现作业
                builder.Services.AddScoped<StartJob>();      // 服务启动作业

                // 配置AutoMapper和AutoFac
                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
            },
            // 注册定时作业
            OnRegist = async scheduler =>
            {
                // 配置指纹采集作业 - 每30秒执行一次
                var fingerJob = JobBuilder.Create<FingerJob>()
                   .WithIdentity("FingerJob", "FingerGroup")
                   .Build();

                var fingerTrigger = TriggerBuilder.Create()
                    .WithIdentity("FingerJob", "FingerGroup")
                    .StartNow()
                    .WithCronSchedule("0/30 * * * * ? ")  // Cron表达式：每30秒触发一次
                    .Build();

                await scheduler.ScheduleJob(fingerJob, fingerTrigger);

                // 配置服务发现作业 - 每1分钟执行一次
                var serviceJob = JobBuilder.Create<ServiceJob>()
                    .WithIdentity("ServiceJob", "ServiceGroup")
                    .Build();

                var discoverTrigger = TriggerBuilder.Create()
                    .WithIdentity("ServiceJob", "ServiceGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/1 * * * ? ")    // Cron表达式：每1分钟触发一次
                    .Build();

                await scheduler.ScheduleJob(serviceJob, discoverTrigger);

                // 配置服务启动作业 - 每10分钟执行一次
                var startJob = JobBuilder.Create<StartJob>()
                    .WithIdentity("StartJob", "StartGroup")
                    .Build();

                var guardTrigger = TriggerBuilder.Create()
                    .WithIdentity("StartJob", "StartGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/10 * * * ? ")   // Cron表达式：每10分钟触发一次
                    .Build();

                await scheduler.ScheduleJob(startJob, guardTrigger);
            }
        };

        await microService.RunAsync(args);
    }
}
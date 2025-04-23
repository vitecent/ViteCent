#region

using Quartz;
using ViteCent.Core.Web;
using ViteCent.Job.Api.Jobs;

#endregion

namespace ViteCent.Job.Api;

/// <summary>
/// </summary>
public class Program
{
    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    public static async Task Main(string[] args)
    {
        var xmls = new List<string>
        {
            "ViteCent.Core.*.xml",
            "ViteCent.Job.*.xml"
        };

        var microService = new JobMicroService("ViteCent.Job.Service", xmls)
        {
            OnBuild = builder =>
            {
                builder.Services.AddScoped<DiscoverJob>();
                builder.Services.AddScoped<GuardJob>();

                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
            },
            OnRegist = async scheduler =>
            {
                var discoverJob = JobBuilder.Create<DiscoverJob>()
                    .WithIdentity("DiscoverJob", "DiscoverGroup")
                    .Build();

                var discoverTrigger = TriggerBuilder.Create()
                    .WithIdentity("DiscoverJob", "DiscoverGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/1 * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(discoverJob, discoverTrigger);

                var guardJob = JobBuilder.Create<GuardJob>()
                    .WithIdentity("GuardJob", "GuardGroup")
                    .Build();

                var guardTrigger = TriggerBuilder.Create()
                    .WithIdentity("GuardJob", "GuardGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/10 * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(guardJob, guardTrigger);
            }
        };

        await microService.RunAsync(args);
    }
}
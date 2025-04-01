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

                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
            },
            OnRegist = async scheduler =>
            {
                var job = JobBuilder.Create<DiscoverJob>()
                    .WithIdentity("DiscoverJob", "DiscoverGroup")
                    .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("DiscoverJob", "DiscoverGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/1 * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        };

        await microService.RunAsync(args);
    }
}
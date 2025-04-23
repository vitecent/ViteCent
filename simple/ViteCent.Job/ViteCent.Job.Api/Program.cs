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
                builder.Services.AddScoped<FingerJob>();
                builder.Services.AddScoped<ServiceJob>();
                builder.Services.AddScoped<StartJob>();

                builder.UseAutoMapper(typeof(AutoMapperConfig));
                builder.UseAutoFac(new AutoFacConfig());
            },
            OnRegist = async scheduler =>
            {
                var fingerJob = JobBuilder.Create<FingerJob>()
                   .WithIdentity("FingerJob", "FingerGroup")
                   .Build();

                var fingerTrigger = TriggerBuilder.Create()
                    .WithIdentity("FingerJob", "FingerGroup")
                    .StartNow()
                    .WithCronSchedule("0/30 * * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(fingerJob, fingerTrigger);

                var serviceJob = JobBuilder.Create<ServiceJob>()
                    .WithIdentity("ServiceJob", "ServiceGroup")
                    .Build();

                var discoverTrigger = TriggerBuilder.Create()
                    .WithIdentity("ServiceJob", "ServiceGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/1 * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(serviceJob, discoverTrigger);

                var startJob = JobBuilder.Create<StartJob>()
                    .WithIdentity("StartJob", "StartGroup")
                    .Build();

                var guardTrigger = TriggerBuilder.Create()
                    .WithIdentity("StartJob", "StartGroup")
                    .StartNow()
                    .WithCronSchedule("0 0/10 * * * ? ")
                    .Build();

                await scheduler.ScheduleJob(startJob, guardTrigger);
            }
        };

        await microService.RunAsync(args);
    }
}
#region

using Quartz;
using ViteCent.Auth.Data.Schedule;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Enums;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// </summary>
/// <param name="logger"></param>
/// <param name="cache"></param>
public class FingerJob(
    ILogger<ServiceJob> logger,
    IBaseCache cache)
    : IJob
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation($"FingerJob : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

        var client = new SqlSugarFactory("ViteCent.Auth");

        var fingser = await client.Query<BaseUserEntity>()
            .Where(x => x.Status == (int)StatusEnum.Enable && !string.IsNullOrWhiteSpace(x.Finger))
            .Select(x => new UserFinger()
            {
                Index = 0,
                Template = x.Finger,
                UserId = x.Id,
                UserName = x.RealName
            })
            .OrderByDescending(x => x.UserId)
            .ToListAsync();

        var index = 1;

        foreach (var item in fingser)
        {
            item.Index = index;

            index++;
        }

        cache.SetString("UserFingers", fingser, TimeSpan.FromSeconds(35));
    }
}
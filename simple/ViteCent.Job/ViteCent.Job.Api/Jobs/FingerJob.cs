#region

using Quartz;
using ViteCent.Auth.Data.BaseUser;
using ViteCent.Auth.Entity.BaseUser;
using ViteCent.Core.Cache;
using ViteCent.Core.Enums;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Job.Api.Jobs;

/// <summary>
/// 指纹数据采集作业 用于定期从数据库获取已启用用户的指纹模板信息，并更新到缓存中，以便其他服务能够及时获取最新的指纹数据
/// </summary>
/// <param name="logger">日志记录器，用于记录作业执行过程中的信息</param>
/// <param name="cache">缓存接口，用于存储指纹数据列表</param>
public class FingerJob(
    ILogger<ServiceJob> logger,
    IBaseCache cache)
    : IJob
{
    /// <summary>
    /// 执行指纹数据采集任务 从数据库中获取所有已启用且已录入指纹的用户信息，为每个用户分配索引号， 并将指纹数据列表更新到缓存中，设置35秒的过期时间
    /// </summary>
    /// <param name="context">作业执行上下文，包含作业的相关信息</param>
    /// <returns>任务</returns>
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
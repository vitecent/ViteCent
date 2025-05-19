#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// Redis分布式锁配置扩展类
/// </summary>
public static class RedisLockExtensions
{
    /// <summary>
    /// 注册Redis分布式锁服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息</param>
    /// <returns>返回服务集合</returns>
    public static IServiceCollection AddRedisLock(this IServiceCollection services, IConfiguration configuration)
    {
        var strConn = configuration["Cache"];

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        services.AddTransient<IBaseLock>(x => new RedisLock(strConn));

        return services;
    }
}
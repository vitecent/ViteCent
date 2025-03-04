#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// </summary>
public static class RedisLockExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedisLock(this IServiceCollection services, IConfiguration configuration)
    {
        var strConn = configuration["Cache"];

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        services.AddTransient<IBaseLock>(x => new RedisLock(strConn));

        return services;
    }
}
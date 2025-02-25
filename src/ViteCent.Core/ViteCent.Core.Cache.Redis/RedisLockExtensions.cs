#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
///     Class RedisLockExtensions.
/// </summary>
public static class RedisLockExtensions
{
    /// <summary>
    ///     Adds the redis lock.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>IServiceCollection.</returns>
    public static IServiceCollection AddRedisLock(this IServiceCollection services, IConfiguration configuration)
    {
        var strConn = configuration["Cache"];

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        services.AddTransient<IBaseLock>(x => new RedisLock(strConn));

        return services;
    }
}
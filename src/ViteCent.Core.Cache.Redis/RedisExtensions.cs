#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// </summary>
public static class RedisExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = BaseLogger.GetLogger();

        var strConn = configuration["Cache"] ?? default!;
        logger.Info($"Redis Config ：{strConn}");

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        services.AddTransient<IBaseCache>(x => new RedisCache(strConn));

        return services;
    }
}
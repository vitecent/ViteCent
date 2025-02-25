#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
///     Redis扩展类。
/// </summary>
public static class RedisExtensions
{
    /// <summary>
    ///     添加Redis缓存服务。
    /// </summary>
    /// <param name="services">服务集合。</param>
    /// <param name="configuration">配置。</param>
    /// <returns>服务集合。</returns>
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
#region

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace ViteCent.Core.Cache.Redis;

/// <summary>
/// Redis缓存配置扩展类
/// </summary>
public static class RedisExtensions
{
    /// <summary>
    /// 注册Redis缓存服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息</param>
    /// <returns>返回服务集合</returns>
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = new BaseLogger(typeof(RedisExtensions));

        var strConn = configuration["Cache"] ?? default!;

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        if (IsEncrypt(configuration))
            strConn = Decrypt(strConn, configuration);

        logger.LogInformation($"Redis Config ：{strConn}");

        services.AddTransient<IBaseCache>(x => new RedisCache(strConn));

        return services;
    }

    /// <summary>
    /// 解密连接字符串
    /// </summary>
    /// <param name="input">加密的连接字符串</param>
    /// <param name="configuration">配置信息</param>
    /// <returns>返回解密后的连接字符串</returns>
    private static string Decrypt(string input, IConfiguration configuration)
    {
        var type = configuration["Encrypt:Type"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(type))
            throw new Exception("Appsettings Must Be Encrypt:Type");

        if (type == "Base64")
        {
            var bytes = input.DecryptBase64();
            return bytes.ByteToString();
        }

        var key = configuration["Encrypt:Key"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Appsettings Must Be Encrypt:Key");

        return type switch
        {
            "AES" => input.DecryptAES(key),
            "DES" => input.DecryptDES(key),
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support")
        };
    }

    /// <summary>
    /// 判断是否启用加密
    /// </summary>
    /// <param name="configuration">配置信息</param>
    /// <returns>返回是否启用加密</returns>
    private static bool IsEncrypt(IConfiguration configuration)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        return true;
    }
}
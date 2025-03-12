#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using ViteCent.Core.Data;

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

        if (string.IsNullOrWhiteSpace(strConn)) throw new Exception("Appsettings Must Be Cache");

        if (IsEncrypt(configuration))
            strConn = Encrypt(strConn, configuration);

        logger.Info($"Redis Config ：{strConn}");

        services.AddTransient<IBaseCache>(x => new RedisCache(strConn));

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static string Encrypt(string input, IConfiguration configuration)
    {
        var type = configuration["Encrypt:Type"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(type))
            throw new Exception("Appsettings Must Be Encrypt:Type");

        if (type == "Base64")
        {
            var bytes = input.StringToByte();
            return bytes.EncryptBase64();
        }

        var key = configuration["Encrypt:Key"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Appsettings Must Be Encrypt:Key");

        return type switch
        {
            "AES" => input.EncryptAES(key),
            "DES" => input.EncryptDES(key),
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support"),
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    private static bool IsEncrypt(IConfiguration configuration)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        return true;
    }
}
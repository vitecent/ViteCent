#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Middlewar;

/// <summary>
/// 请求解密中间件，用于处理加密的HTTP请求内容
/// 支持Base64、AES和DES解密方式，通过配置文件控制解密行为
/// </summary>
/// <param name="next">请求处理管道中的下一个中间件委托</param>
/// <param name="configuration">应用程序配置接口，用于获取解密相关的配置信息</param>
public class BaseDecryptRequestMiddlewar(RequestDelegate next, IConfiguration configuration)
{
    /// <summary>
    /// 处理HTTP请求的异步方法
    /// 当请求需要解密时，读取请求体内容并进行解密处理
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>异步任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        if (IsEncrypt(context))
        {
            context.Request.EnableBuffering();

            var encryptedBody = await new StreamReader(context.Request.Body).ReadToEndAsync();
            var bytes = Decrypt(encryptedBody);
            context.Request.Body = new MemoryStream(bytes);
            context.Request.Body.Seek(0, SeekOrigin.Begin);
            context.Request.ContentLength = bytes.Length;
        }

        await next(context);
    }

    /// <summary>
    /// 解密请求内容的私有方法
    /// 根据配置的加密类型选择相应的解密方式
    /// </summary>
    /// <param name="input">需要解密的字符串内容</param>
    /// <returns>解密后的字节数组</returns>
    /// <exception cref="Exception">当配置缺失或加密类型不支持时抛出异常</exception>
    private byte[] Decrypt(string input)
    {
        var type = configuration["Encrypt:Type"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(type))
            throw new Exception("Appsettings Must Be Encrypt:Type");

        if (type == "Base64")
            return input.DecryptBase64();

        var key = configuration["Encrypt:Key"] ?? string.Empty;

        if (string.IsNullOrWhiteSpace(key))
            throw new Exception("Appsettings Must Be Encrypt:Key");

        var result = type switch
        {
            "AES" => input.DecryptAES(key),
            "DES" => input.DecryptDES(key),
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support")
        };

        return result.StringToByte();
    }

    /// <summary>
    /// 判断当前请求是否需要解密处理
    /// 通过配置开关和请求路径判断
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>如果需要解密则返回true，否则返回false</returns>
    private bool IsEncrypt(HttpContext context)
    {
        var _switch = configuration["Encrypt:Switch"] ?? string.Empty;

        var result = bool.TryParse(_switch, out var flag);

        if (!result || !flag)
            return false;

        var check = configuration["Service:Check"];

        if (string.IsNullOrWhiteSpace(check)) check = BaseConst.Check;

        return context.Request.Path.StartsWithSegments("/openapi") || context.Request.Path.StartsWithSegments(check);
    }
}
#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using System.Text;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Middlewar;

/// <summary>
/// 响应加密中间件，用于对API响应内容进行加密处理
/// 支持Base64、AES和DES三种加密方式
/// </summary>
/// <param name="next">请求处理管道中的下一个中间件</param>
/// <param name="configuration">应用程序配置，用于获取加密相关的配置项</param>
public class BaseEncryptResponseMiddlewar(RequestDelegate next, IConfiguration configuration)
{
    /// <summary>
    /// 处理HTTP响应的异步方法
    /// 当响应需要加密时，读取响应体内容并进行加密处理
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>异步任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (IsEncrypt(context))
        {
            var originalBody = context.Response.Body;
            using var newBody = new MemoryStream();
            context.Response.Body = newBody;

            newBody.Seek(0, SeekOrigin.Begin);
            var plainResponse = await new StreamReader(newBody).ReadToEndAsync();
            var encryptedResponse = Encrypt(plainResponse);

            context.Response.Body = originalBody;
            context.Response.ContentLength = Encoding.UTF8.GetByteCount(encryptedResponse);
            await context.Response.WriteAsync(encryptedResponse);
        }
    }

    /// <summary>
    /// 加密响应内容的私有方法
    /// 根据配置的加密类型选择相应的加密方式
    /// </summary>
    /// <param name="input">需要加密的字符串内容</param>
    /// <returns>加密后的字符串</returns>
    /// <exception cref="Exception">当配置缺失或加密类型不支持时抛出异常</exception>
    private string Encrypt(string input)
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
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support")
        };
    }

    /// <summary>
    /// 判断当前请求是否需要加密响应的私有方法
    /// 根据配置和请求路径判断是否需要进行响应加密
    /// </summary>
    /// <param name="context">当前HTTP请求的上下文信息</param>
    /// <returns>如果需要加密则返回true，否则返回false</returns>
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
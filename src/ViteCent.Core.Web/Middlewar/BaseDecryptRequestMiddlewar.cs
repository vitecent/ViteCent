#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Middlewar;

/// <summary>
/// </summary>
/// <param name="next"></param>
/// <param name="configuration"></param>
public class BaseDecryptRequestMiddlewar(RequestDelegate next, IConfiguration configuration)
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support"),
        };

        return result.StringToByte();
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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
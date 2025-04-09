#region

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core.Web.Middlewar;

/// <summary>
/// </summary>
/// <param name="next"></param>
/// <param name="configuration"></param>
public class BaseEncryptResponseMiddlewar(RequestDelegate next, IConfiguration configuration)
{
    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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
            _ => throw new Exception($"Encrypt:Type {type} Is Not Support"),
        };
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
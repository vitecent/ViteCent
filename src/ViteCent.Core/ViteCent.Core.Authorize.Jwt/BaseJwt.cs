using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViteCent.Core.Data;

namespace ViteCent.Core.Authorize.Jwt;

/// <summary>
/// JWT令牌生成工具类，提供JWT令牌的创建和管理功能
/// </summary>
public class BaseJwt
{
    /// <summary>
    /// 生成JWT令牌
    /// </summary>
    /// <param name="user">用户信息对象，包含需要写入令牌的用户数据</param>
    /// <param name="configuration">配置对象，用于获取JWT相关配置项（Key、Issuer、Audience、Expires等）</param>
    /// <returns>返回生成的JWT令牌字符串</returns>
    public static string GenerateJwtToken(BaseUserInfo user, IConfiguration configuration)
    {
        var logger = new BaseLogger(typeof(BaseJwt));

        var key = configuration["Jwt:Key"] ?? default!;

        if (string.IsNullOrWhiteSpace(key)) throw new Exception("Appsettings Must Be Jwt:Key");

        logger.LogInformation($"Jwt Key ：{key}");

        var issuer = configuration["Jwt:Issuer"] ?? default!;

        if (string.IsNullOrWhiteSpace(issuer)) throw new Exception("Appsettings Must Be Jwt:Issuer");

        logger.LogInformation($"Jwt Issuer ：{issuer}");

        var audience = configuration["Jwt:Audience"] ?? default!;

        if (string.IsNullOrWhiteSpace(audience)) throw new Exception("Appsettings Must Be Jwt:Audience");

        logger.LogInformation($"Jwt Audience ：{audience}");

        var flagExpires = int.TryParse(configuration["Jwt:Expires"] ?? default!, out var expires);

        if (!flagExpires || expires < 1) expires = 24;

        logger.LogInformation($"Jwt Expires ：{expires}");

        var claims = new[]
        {
            new Claim(ClaimTypes.UserData, user.ToJson())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer,
            audience,
            claims,
            expires: DateTime.Now.AddHours(expires),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
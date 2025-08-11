using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace ViteCent.Core.Authorize.Jwt;

/// <summary>
/// JWT认证配置扩展类，提供JWT认证服务的注册和中间件配置
/// </summary>
public static class JwtExtensions
{
    /// <summary>
    /// 添加JWT认证服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置信息，用于获取JWT相关配置项（Key、Issuer、Audience）</param>
    /// <returns>返回服务集合以支持链式调用</returns>
    public static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = new BaseLogger(typeof(JwtExtensions));

        var key = configuration["Jwt:Key"] ?? default!;

        if (string.IsNullOrWhiteSpace(key)) throw new Exception("Appsettings Must Be Jwt:Key");

        logger.LogInformation($"Jwt Key ：{key}");

        var issuer = configuration["Jwt:Issuer"] ?? default!;

        if (string.IsNullOrWhiteSpace(issuer)) throw new Exception("Appsettings Must Be Jwt:Issuer");

        logger.LogInformation($"Jwt Issuer ：{issuer}");

        var audience = configuration["Jwt:Audience"] ?? default!;

        if (string.IsNullOrWhiteSpace(audience)) throw new Exception("Appsettings Must Be Jwt:Audience");

        logger.LogInformation($"Jwt Audience ：{audience}");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

        return services;
    }

    /// <summary>
    /// 启用JWT认证中间件
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>返回应用程序构建器以支持链式调用</returns>
    public static IApplicationBuilder UseJwt(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace ViteCent.Core.Authorize.Jwt;

/// <summary>
/// </summary>
public static class JwtExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseJwt(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Web.Middlewar;

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public static class BaseEncryptResponseExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddEncryptResponse(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static void UseEncryptResponse(this WebApplication app)
    {
        app.UseMiddleware<BaseEncryptResponseMiddlewar>();
    }
}
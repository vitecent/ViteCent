#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Web.Middlewar;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// </summary>
public static class BaseGatewayExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddGateway(this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static void UseGateway(this WebApplication app)
    {
        app.UseMiddleware<BaseGatewayMiddlewar>();
    }
}
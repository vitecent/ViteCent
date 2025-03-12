#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace ViteCent.Core.Api.Swagger;

/// <summary>
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="title"></param>
    /// <param name="xmls"></param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string title, List<string> xmls)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        return services;
    }

    /// <summary>
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        return app;
    }
}
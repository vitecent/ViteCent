#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

#endregion

namespace ViteCent.Core.Api.Swagger;

/// <summary>
/// Swagger API文档配置扩展类，提供配置和启用Swagger文档功能
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// 添加Swagger服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="title">API文档标题</param>
    /// <param name="xmls">XML文档注释文件路径列表</param>
    /// <returns>配置后的服务集合</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string title, List<string> xmls)
    {
        services.AddEndpointsApiExplorer();
        services.AddOpenApi();

        return services;
    }

    /// <summary>
    /// 启用Swagger中间件，仅在开发环境中启用
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>配置后的应用程序构建器</returns>
    public static IApplicationBuilder UseSwagger(this WebApplication app)
    {
        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        return app;
    }
}
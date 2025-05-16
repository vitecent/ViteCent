#region

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Web.Middlewar;

#endregion

namespace ViteCent.Core.Web;

/// <summary>
/// 网关中间件扩展类，提供配置和启用网关中间件的功能
/// </summary>
public static class BaseGatewayExtensions
{
    /// <summary>
    /// 添加网关服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>配置后的服务集合</returns>
    public static IServiceCollection AddGateway(this IServiceCollection services)
    {
        services.AddHttpClient();

        return services;
    }

    /// <summary>
    /// 启用网关中间件
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>无返回值</returns>
    public static void UseGateway(this WebApplication app)
    {
        app.UseMiddleware<BaseGatewayMiddlewar>();
    }
}
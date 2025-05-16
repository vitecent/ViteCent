using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Web.Middlewar;

namespace ViteCent.Core.Web;

/// <summary>
/// 响应加密中间件扩展类，提供配置和启用响应加密中间件的功能
/// </summary>
public static class BaseEncryptResponseExtensions
{
    /// <summary>
    /// 添加响应加密服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>配置后的服务集合</returns>
    public static IServiceCollection AddEncryptResponse(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// 启用响应加密中间件
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    /// <returns>无返回值</returns>
    public static void UseEncryptResponse(this WebApplication app)
    {
        app.UseMiddleware<BaseEncryptResponseMiddlewar>();
    }
}
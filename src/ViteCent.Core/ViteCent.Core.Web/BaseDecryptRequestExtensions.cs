using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ViteCent.Core.Web.Middlewar;

namespace ViteCent.Core.Web;

/// <summary>
/// 请求解密中间件扩展类，提供配置和启用请求解密中间件的功能
/// </summary>
public static class BaseDecryptRequestExtensions
{
    /// <summary>
    /// 添加请求解密服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>配置后的服务集合</returns>
    public static IServiceCollection AddDecryptRequest(this IServiceCollection services)
    {
        return services;
    }

    /// <summary>
    /// 启用请求解密中间件
    /// </summary>
    /// <param name="app">Web应用程序构建器</param>
    public static void UseDecryptRequest(this WebApplication app)
    {
        app.UseMiddleware<BaseDecryptRequestMiddlewar>();
    }
}
#region

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace ViteCent.Core.Logging.Log4Net;

/// <summary>
/// Log4Net日志配置扩展类，提供配置和启用Log4Net日志功能
/// </summary>
public static class Log4NetExtensions
{
    /// <summary>
    /// 添加Log4Net日志服务到依赖注入容器
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>无返回值</returns>
    public static void AddLog4Net(this IServiceCollection services)
    {
        services.AddLogging(configuration =>
        {
            configuration.ClearProviders();
            configuration.AddLog4Net();
        });
    }
}
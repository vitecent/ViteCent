#region

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace ViteCent.Core.Logging.Log4Net;

/// <summary>
/// </summary>
public static class Log4NetExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    public static void AddLog4Net(this IServiceCollection services)
    {
        services.AddLogging(configuration =>
        {
            configuration.ClearProviders();
            configuration.AddLog4Net();
        });
    }
}
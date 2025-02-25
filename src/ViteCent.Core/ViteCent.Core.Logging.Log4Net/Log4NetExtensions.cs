#region

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

#endregion

namespace ViteCent.Core.Logging.Log4Net;

/// <summary>
///     Class Log4NetExtensions.
/// </summary>
public static class Log4NetExtensions
{
    /// <summary>
    ///     Uses the log4 net.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddLog4Net(this IServiceCollection services)
    {
        services.AddLogging(configuration =>
        {
            configuration.ClearProviders();
            configuration.AddLog4Net();
        });
    }
}
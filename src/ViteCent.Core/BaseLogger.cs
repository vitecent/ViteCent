#region

using log4net;
using log4net.Config;
using System.Reflection;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseLogger
{
    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static ILog GetLogger(Type? type = null)
    {
        var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        var log = LogManager.GetLogger(type ?? typeof(BaseLogger));

        return log;
    }
}
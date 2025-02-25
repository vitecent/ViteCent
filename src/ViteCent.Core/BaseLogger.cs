#region

using log4net;
using log4net.Config;

using System.Reflection;

#endregion

namespace ViteCent.Core;

/// <summary>
///     日志记录器基类
/// </summary>
public static class BaseLogger
{
    /// <summary>
    ///     获取日志记录器
    /// </summary>
    /// <returns></returns>
    public static ILog GetLogger()
    {
        var logRep = LogManager.GetRepository(Assembly.GetEntryAssembly());
        XmlConfigurator.Configure(logRep, new FileInfo("log4net.config"));

        // 获取日志记录器
        var log = LogManager.GetLogger("Log4NetExtensions");

        return log;
    }
}
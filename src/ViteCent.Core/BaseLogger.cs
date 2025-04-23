#region

using System.Reflection;
using log4net;
using log4net.Config;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public class BaseLogger
{
    /// <summary>
    /// </summary>
    private readonly ILog logger;

    /// <summary>
    /// </summary>
    /// <param name="type"></param>
    public BaseLogger(Type? type)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new Exception("Entry assembly cannot be null.");
        var logRepository = LogManager.GetRepository(assembly);

        var log4NetConfig = new FileInfo("log4net.config");
        XmlConfigurator.ConfigureAndWatch(logRepository, log4NetConfig);

        logger = LogManager.GetLogger(type ?? typeof(BaseLogger));
    }

    /// <summary>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="message"></param>
    public void LogDebug(Exception exception, string message)
    {
        logger.Debug(message, exception);
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public void LogDebug(string message)
    {
        logger.Debug(message);
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public void LogError(string message)
    {
        logger.Error(message);
    }

    /// <summary>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="message"></param>
    public void LogError(Exception exception, string message)
    {
        logger.Error(message, exception);
    }

    /// <summary>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="message"></param>
    public void LogInformation(Exception exception, string message)
    {
        logger.Info(message, exception);
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public void LogInformation(string message)
    {
        logger.Info(message);
    }

    /// <summary>
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="message"></param>
    public void LogWarning(Exception exception, string message)
    {
        logger.Warn(message, exception);
    }

    /// <summary>
    /// </summary>
    /// <param name="message"></param>
    public void LogWarning(string message)
    {
        logger.Warn(message);
    }
}
#region

using log4net;
using log4net.Config;

using System.Reflection;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 基于log4net的日志记录工具类，提供统一的日志记录接口
/// </summary>
public class BaseLogger
{
    /// <summary>
    /// log4net日志记录器实例
    /// </summary>
    private readonly ILog logger;

    /// <summary>
    /// 初始化日志记录器
    /// </summary>
    /// <param name="type">记录日志的类型，如果为null则使用BaseLogger类型</param>
    public BaseLogger(Type? type)
    {
        var assembly = Assembly.GetEntryAssembly() ?? throw new Exception("Entry assembly cannot be null.");
        var logRepository = LogManager.GetRepository(assembly);

        var log4NetConfig = new FileInfo("log4net.config");
        XmlConfigurator.ConfigureAndWatch(logRepository, log4NetConfig);

        logger = LogManager.GetLogger(type ?? typeof(BaseLogger));
    }

    /// <summary>
    /// 记录调试级别的日志信息
    /// </summary>
    /// <param name="exception">异常信息</param>
    /// <param name="message">日志消息</param>
    public void LogDebug(Exception exception, string message)
    {
        logger.Debug(message, exception);
    }

    /// <summary>
    /// 记录调试级别的日志信息
    /// </summary>
    /// <param name="message">日志消息</param>
    public void LogDebug(string message)
    {
        logger.Debug(message);
    }

    /// <summary>
    /// 记录错误级别的日志信息
    /// </summary>
    /// <param name="message">日志消息</param>
    public void LogError(string message)
    {
        logger.Error(message);
    }

    /// <summary>
    /// 记录错误级别的日志信息
    /// </summary>
    /// <param name="exception">异常信息</param>
    /// <param name="message">日志消息</param>
    public void LogError(Exception exception, string message)
    {
        logger.Error(message, exception);
    }

    /// <summary>
    /// 记录信息级别的日志信息
    /// </summary>
    /// <param name="exception">异常信息</param>
    /// <param name="message">日志消息</param>
    public void LogInformation(Exception exception, string message)
    {
        logger.Info(message, exception);
    }

    /// <summary>
    /// 记录信息级别的日志信息
    /// </summary>
    /// <param name="message">日志消息</param>
    public void LogInformation(string message)
    {
        logger.Info(message);
    }

    /// <summary>
    /// 记录警告级别的日志信息
    /// </summary>
    /// <param name="exception">异常信息</param>
    /// <param name="message">日志消息</param>
    public void LogWarning(Exception exception, string message)
    {
        logger.Warn(message, exception);
    }

    /// <summary>
    /// 记录警告级别的日志信息
    /// </summary>
    /// <param name="message">日志消息</param>
    public void LogWarning(string message)
    {
        logger.Warn(message);
    }
}
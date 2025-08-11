namespace ViteCent.Core;

/// <summary>
/// 日期时间工具类，提供常用的日期时间处理功能
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// Unix时间戳的起始时间（1970年1月1日）
    /// </summary>
    private static readonly DateTime mindate = Convert.ToDateTime("1970/01/01 00:00:00");

    /// <summary>
    /// 将Unix时间戳转换为DateTime类型
    /// </summary>
    /// <param name="unix">Unix时间戳（毫秒）</param>
    /// <returns>转换后的DateTime对象</returns>
    public static DateTime FromUnix(this double unix)
    {
        return mindate.AddMilliseconds(unix);
    }

    /// <summary>
    /// 将字符串转换为DateTime类型，如果转换失败则返回当前时间
    /// </summary>
    /// <param name="input">要转换的日期时间字符串</param>
    /// <returns>转换后的DateTime对象</returns>
    public static DateTime GetDateTime(this string input)
    {
        return input.GetDateTime(DateTime.Now);
    }

    /// <summary>
    /// 将字符串转换为DateTime类型，如果转换失败则返回指定的默认值
    /// </summary>
    /// <param name="input">要转换的日期时间字符串</param>
    /// <param name="defaultValue">转换失败时返回的默认值</param>
    /// <returns>转换后的DateTime对象</returns>
    public static DateTime GetDateTime(this string input, DateTime defaultValue)
    {
        return DateTime.TryParse(input, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// 获取指定日期的当天结束时间（23:59:59）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当天的结束时间</returns>
    public static DateTime ToDayEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-dd 23:59:59"));
    }

    /// <summary>
    /// 获取指定日期的当天开始时间（00:00:00）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当天的开始时间</returns>
    public static DateTime ToDayStart(this DateTime time)
    {
        return time.Date;
    }

    /// <summary>
    /// 获取指定日期的当月最后一天的结束时间（23:59:59）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当月最后一天的结束时间</returns>
    public static DateTime ToMonthEnd(this DateTime time)
    {
        var days = DateTime.DaysInMonth(time.Year, time.Month).ToString();

        if (days.Length == 1) days = $"0{days}";

        return DateTime.Parse(time.ToString($"yyyy-MM-{days} 23:59:59"));
    }

    /// <summary>
    /// 获取指定日期的当月第一天的开始时间（00:00:00）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当月第一天的开始时间</returns>
    public static DateTime ToMonthStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-01 00:00:00"));
    }

    /// <summary>
    /// 将DateTime转换为Unix时间戳（毫秒）
    /// </summary>
    /// <param name="time">要转换的DateTime对象</param>
    /// <returns>Unix时间戳（毫秒）</returns>
    public static double ToUnix(this DateTime time)
    {
        return (time - mindate).TotalMilliseconds;
    }

    /// <summary>
    /// 获取指定日期的当年最后一天的结束时间（23:59:59）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当年最后一天的结束时间</returns>
    public static DateTime ToYearEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-12-31 23:59:59"));
    }

    /// <summary>
    /// 获取指定日期的当年第一天的开始时间（00:00:00）
    /// </summary>
    /// <param name="time">要处理的日期时间</param>
    /// <returns>当年第一天的开始时间</returns>
    public static DateTime ToYearStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-01-01 00:00:00"));
    }
}
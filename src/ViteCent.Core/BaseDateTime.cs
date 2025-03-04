namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    /// </summary>
    private static readonly DateTime mindate = Convert.ToDateTime("1970/01/01 00:00:00");

    /// <summary>
    /// </summary>
    /// <param name="unix"></param>
    /// <returns></returns>
    public static DateTime FromUnix(this double unix)
    {
        return mindate.AddMilliseconds(unix);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(this string str)
    {
        return str.GetDateTime(default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static DateTime GetDateTime(this string str, DateTime defaultValue)
    {
        return DateTime.TryParse(str, out var value) ? value : defaultValue;
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToDayEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-dd 23:59:59"));
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToDayStart(this DateTime time)
    {
        return time.Date;
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToMonthEnd(this DateTime time)
    {
        var days = DateTime.DaysInMonth(time.Year, time.Month).ToString();

        if (days.Length == 1) days = $"0{days}";

        return DateTime.Parse(time.ToString($"yyyy-MM-{days} 23:59:59"));
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToMonthStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-01 00:00:00"));
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static double ToUnix(this DateTime time)
    {
        return (time - mindate).TotalMilliseconds;
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToYearEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-12-31 23:59:59"));
    }

    /// <summary>
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public static DateTime ToYearStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-01-01 00:00:00"));
    }
}
namespace ViteCent.Core;

/// <summary>
///     Class DateTimeHelper.
/// </summary>
public static class DateTimeHelper
{
    /// <summary>
    ///     The mindate
    /// </summary>
    private static readonly DateTime mindate = Convert.ToDateTime("1970/01/01 00:00:00");

    /// <summary>
    ///     Froms the unix.
    /// </summary>
    /// <param name="unix">The unix.</param>
    /// <returns>DateTime.</returns>
    public static DateTime FromUnix(this double unix)
    {
        return mindate.AddMilliseconds(unix);
    }

    /// <summary>
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>DateTime.</returns>
    public static DateTime GetDateTime(this string str)
    {
        return str.GetDateTime(default);
    }

    /// <summary>
    ///     Gets the date time.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>DateTime.</returns>
    public static DateTime GetDateTime(this string str, DateTime defaultValue)
    {
        return DateTime.TryParse(str, out var value) ? value : defaultValue;
    }

    /// <summary>
    ///     Converts to dayend.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToDayEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-dd 23:59:59"));
    }

    /// <summary>
    ///     Converts to daystart.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToDayStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-dd 00:00:00"));
    }

    /// <summary>
    ///     Converts to monthend.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToMonthEnd(this DateTime time)
    {
        var days = DateTime.DaysInMonth(time.Year, time.Month).ToString();

        if (days.Length == 1) days = $"0{days}";

        return DateTime.Parse(time.ToString($"yyyy-MM-{days} 23:59:59"));
    }

    /// <summary>
    ///     Converts to monthstart.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToMonthStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-MM-01 00:00:00"));
    }

    /// <summary>
    ///     Converts to unix.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>System.Double.</returns>
    public static double ToUnix(this DateTime time)
    {
        return (time - mindate).TotalMilliseconds;
    }

    /// <summary>
    ///     Converts to yearend.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToYearEnd(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-12-31 23:59:59"));
    }

    /// <summary>
    ///     Converts to yearstart.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <returns>DateTime.</returns>
    public static DateTime ToYearStart(this DateTime time)
    {
        return DateTime.Parse(time.ToString("yyyy-01-01 00:00:00"));
    }
}
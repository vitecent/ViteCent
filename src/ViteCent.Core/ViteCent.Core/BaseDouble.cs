namespace ViteCent.Core;

/// <summary>
///     DoubleHelper
/// </summary>
public static class DoubleHelper
{
    /// <summary>
    ///     Gets the double.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.Double.</returns>
    public static double GetDouble(this string str)
    {
        return str.GetDouble(default);
    }

    /// <summary>
    ///     Gets the double.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>System.Double.</returns>
    public static double GetDouble(this string str, double defaultValue)
    {
        return double.TryParse(str, out var value) ? value : defaultValue;
    }
}
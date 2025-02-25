namespace ViteCent.Core;

/// <summary>
///     Class IntHelper.
/// </summary>
public static class IntHelper
{
    /// <summary>
    ///     Gets the int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.Int32.</returns>
    public static int GetInt(this string str)
    {
        return str.GetInt(default);
    }

    /// <summary>
    ///     Gets the int.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="defaultValue">The default value.</param>
    /// <returns>System.Int32.</returns>
    public static int GetInt(this string str, int defaultValue)
    {
        return int.TryParse(str, out var value) ? value : defaultValue;
    }
}
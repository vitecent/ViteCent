namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class DoubleHelper
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static double GetDouble(this string str)
    {
        return str.GetDouble(default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double GetDouble(this string str, double defaultValue)
    {
        return double.TryParse(str, out var value) ? value : defaultValue;
    }
}
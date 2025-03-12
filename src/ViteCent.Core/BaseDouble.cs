namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class DoubleHelper
{
    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static double GetDouble(this string input)
    {
        return input.GetDouble(0);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static double GetDouble(this string input, double defaultValue)
    {
        return double.TryParse(input, out var value) ? value : defaultValue;
    }
}
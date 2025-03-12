namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class IntHelper
{
    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static int GetInt(this string input)
    {
        return input.GetInt(0);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(this string input, int defaultValue)
    {
        return int.TryParse(input, out var value) ? value : defaultValue;
    }
}
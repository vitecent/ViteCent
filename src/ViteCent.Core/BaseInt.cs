namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class IntHelper
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int GetInt(this string str)
    {
        return str.GetInt(default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static int GetInt(this string str, int defaultValue)
    {
        return int.TryParse(str, out var value) ? value : defaultValue;
    }
}
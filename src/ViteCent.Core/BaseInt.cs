namespace ViteCent.Core;

/// <summary>
/// 提供字符串转整数的扩展方法
/// </summary>
public static class IntHelper
{
    /// <summary>
    /// 将字符串转换为整数，如果转换失败则返回0
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <returns>转换后的整数，转换失败时返回0</returns>
    public static int GetInt(this string input)
    {
        return input.GetInt(0);
    }

    /// <summary>
    /// 将字符串转换为整数，如果转换失败则返回指定的默认值
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <param name="defaultValue">转换失败时返回的默认值</param>
    /// <returns>转换后的整数，转换失败时返回默认值</returns>
    public static int GetInt(this string input, int defaultValue)
    {
        return int.TryParse(input, out var value) ? value : defaultValue;
    }
}
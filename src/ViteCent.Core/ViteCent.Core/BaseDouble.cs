namespace ViteCent.Core;

/// <summary>
/// 提供字符串转双精度浮点数的扩展方法
/// </summary>
public static class DoubleHelper
{
    /// <summary>
    /// 将字符串转换为双精度浮点数，如果转换失败则返回0
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <returns>转换后的双精度浮点数，转换失败时返回0</returns>
    public static double GetDouble(this string input)
    {
        return input.GetDouble(0);
    }

    /// <summary>
    /// 将字符串转换为双精度浮点数，如果转换失败则返回指定的默认值
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <param name="defaultValue">转换失败时返回的默认值</param>
    /// <returns>转换后的双精度浮点数，转换失败时返回默认值</returns>
    public static double GetDouble(this string input, double defaultValue)
    {
        return double.TryParse(input, out var value) ? value : defaultValue;
    }
}
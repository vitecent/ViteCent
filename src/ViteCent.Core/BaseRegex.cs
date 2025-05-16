#region

using System.Text.RegularExpressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static partial class BaseRegex
{
    /// <summary>
    /// 验证字符串是否只包含中文字符
    /// </summary>
    /// <param name="input">待验证的字符串</param>
    /// <returns>如果字符串只包含中文字符返回true，否则返回false</returns>
    public static bool IsChinese(this string input)
    {
        return Chinese().IsMatch(input);
    }

    /// <summary>
    /// 验证字符串是否只包含中文和英文字符
    /// </summary>
    /// <param name="input">待验证的字符串</param>
    /// <returns>如果字符串只包含中文和英文字符返回true，否则返回false</returns>
    public static bool IsChineseEnglish(this string input)
    {
        return ChineseEnglish().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsChineseEnglishUnderline(this string input)
    {
        return ChineseEnglishUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsChineseUnderline(this string input)
    {
        return ChineseUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsDecimal(this string input, int length = 2)
    {
        return Regex.IsMatch(input, string.Format(BaseConst.Decimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsEmail(this string input)
    {
        return Email().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsEnglish(this string input)
    {
        return English().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsIdCard(this string input, int length = 18)
    {
        switch (length)
        {
            case 18:
                {
                    return input.IsIdCard18();
                }
            case 15:
                {
                    return input.IsIdCard15();
                }
            default:
                {
                    return false;
                }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsIP(this string input)
    {
        return IP().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static bool IsMatch(this string input, string regex)
    {
        return Regex.IsMatch(input, regex);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsMobile(this string input)
    {
        return Mobile().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsNegative(this string input)
    {
        return Negative().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsNegativeDecimal(this string input, int length = 2)
    {
        return Regex.IsMatch(input, string.Format(BaseConst.NegativeDecimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositive(this string input)
    {
        return Positive().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveChinese(this string input)
    {
        return PositiveChinese().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseEnglish(this string input)
    {
        return PositiveChineseEnglish().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseEnglishUnderline(this string input)
    {
        return PositiveChineseEnglishUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseUnderline(this string input)
    {
        return PositiveChineseUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsPositiveDecimal(this string input, int length = 2)
    {
        return Regex.IsMatch(input, string.Format(BaseConst.PositiveDecimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveEnglish(this string input)
    {
        return PositiveEnglish().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveEnglishUnderline(this string input)
    {
        return PositiveEnglishUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveNegative(this string input)
    {
        return PositiveNegative().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsPositiveUnderline(this string input)
    {
        return PositiveUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool IsUrl(this string input)
    {
        return Url().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool Underline(this string input)
    {
        return EnglishUnderline().IsMatch(input);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Chinese)]
    private static partial Regex Chinese();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.ChineseEnglish)]
    private static partial Regex ChineseEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.ChineseEnglishUnderline)]
    private static partial Regex ChineseEnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.ChineseUnderline)]
    private static partial Regex ChineseUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Email)]
    private static partial Regex Email();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.English)]
    private static partial Regex English();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.EnglishUnderline)]
    private static partial Regex EnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.IP)]
    private static partial Regex IP();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Mobile)]
    private static partial Regex Mobile();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Negative)]
    private static partial Regex Negative();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Positive)]
    private static partial Regex Positive();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveChinese)]
    private static partial Regex PositiveChinese();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveChineseEnglish)]
    private static partial Regex PositiveChineseEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveChineseEnglishUnderline)]
    private static partial Regex PositiveChineseEnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveChineseUnderline)]
    private static partial Regex PositiveChineseUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveEnglish)]
    private static partial Regex PositiveEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveEnglishUnderline)]
    private static partial Regex PositiveEnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveNegative)]
    private static partial Regex PositiveNegative();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.PositiveUnderline)]
    private static partial Regex PositiveUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(BaseConst.Url)]
    private static partial Regex Url();
}
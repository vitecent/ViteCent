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
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChinese(this string str)
    {
        return Chinese().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChineseEnglish(this string str)
    {
        return ChineseEnglish().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsChineseUnderline(this string str)
    {
        return ChineseUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.Decimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEmail(this string str)
    {
        return Email().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsEnglish(this string str)
    {
        return English().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsIdCard(this string str, int length = 18)
    {
        switch (length)
        {
            case 18:
                {
                    return str.IsIdCard18();
                }
            case 15:
                {
                    return str.IsIdCard15();
                }
            default:
                {
                    return false;
                }
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsIP(this string str)
    {
        return IP().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static bool IsMatch(this string str, string regex)
    {
        return Regex.IsMatch(str, regex);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsMobile(this string str)
    {
        return Mobile().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsNegative(this string str)
    {
        return Negative().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsNegativeDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.NegativeDecimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsNegativeDecimalDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.NegativeDecimalDecimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositive(this string str)
    {
        return Positive().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveChinese(this string str)
    {
        return PositiveChinese().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseEnglish(this string str)
    {
        return PositiveChineseEnglish().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseEnglishUnderline(this string str)
    {
        return PositiveChineseEnglishUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveChineseUnderline(this string str)
    {
        return PositiveChineseUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static bool IsPositiveDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.PositiveDecimal, length));
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveEnglish(this string str)
    {
        return PositiveEnglish().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveEnglishUnderline(this string str)
    {
        return PositiveEnglishUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveNegative(this string str)
    {
        return PositiveNegative().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsPositiveUnderline(this string str)
    {
        return PositiveUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsUrl(this string str)
    {
        return Url().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool Underline(this string str)
    {
        return EnglishUnderline().IsMatch(str);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Chinese)]
    private static partial Regex Chinese();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.ChineseEnglish)]
    private static partial Regex ChineseEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.ChineseUnderline)]
    private static partial Regex ChineseUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Email)]
    private static partial Regex Email();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.English)]
    private static partial Regex English();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.EnglishUnderline)]
    private static partial Regex EnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.IP)]
    private static partial Regex IP();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Mobile)]
    private static partial Regex Mobile();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Negative)]
    private static partial Regex Negative();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Positive)]
    private static partial Regex Positive();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveChinese)]
    private static partial Regex PositiveChinese();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveChineseEnglish)]
    private static partial Regex PositiveChineseEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveChineseEnglishUnderline)]
    private static partial Regex PositiveChineseEnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveChineseUnderline)]
    private static partial Regex PositiveChineseUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveEnglish)]
    private static partial Regex PositiveEnglish();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveEnglishUnderline)]
    private static partial Regex PositiveEnglishUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveNegative)]
    private static partial Regex PositiveNegative();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.PositiveUnderline)]
    private static partial Regex PositiveUnderline();

    /// <summary>
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex(Const.Url)]
    private static partial Regex Url();
}
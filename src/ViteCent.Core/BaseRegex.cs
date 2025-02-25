#region

using System.Text.RegularExpressions;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class RegexExtensions.
/// </summary>
public static partial class BaseRegex
{
    /// <summary>
    ///     Determines whether the specified string is chinese.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is chinese; otherwise, <c>false</c>.</returns>
    public static bool IsChinese(this string str)
    {
        return Chinese().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is chinese english] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is chinese english] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsChineseEnglish(this string str)
    {
        return ChineseEnglish().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is chinese underline] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is chinese underline] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsChineseUnderline(this string str)
    {
        return ChineseUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether the specified length is decimal.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="length">The length.</param>
    /// <returns><c>true</c> if the specified length is decimal; otherwise, <c>false</c>.</returns>
    public static bool IsDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.Decimal, length));
    }

    /// <summary>
    ///     Determines whether the specified string is email.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is email; otherwise, <c>false</c>.</returns>
    public static bool IsEmail(this string str)
    {
        return Email().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether the specified string is english.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is english; otherwise, <c>false</c>.</returns>
    public static bool IsEnglish(this string str)
    {
        return English().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is identifier card] [the specified length].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="length">The length.</param>
    /// <returns><c>true</c> if [is identifier card] [the specified length]; otherwise, <c>false</c>.</returns>
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
    ///     Determines whether the specified string is ip.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is ip; otherwise, <c>false</c>.</returns>
    public static bool IsIP(this string str)
    {
        return IP().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether the specified regex is match.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="regex">The regex.</param>
    /// <returns><c>true</c> if the specified regex is match; otherwise, <c>false</c>.</returns>
    public static bool IsMatch(this string str, string regex)
    {
        return Regex.IsMatch(str, regex);
    }

    /// <summary>
    ///     Determines whether the specified string is mobile.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is mobile; otherwise, <c>false</c>.</returns>
    public static bool IsMobile(this string str)
    {
        return Mobile().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether the specified string is negative.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is negative; otherwise, <c>false</c>.</returns>
    public static bool IsNegative(this string str)
    {
        return Negative().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is negative decimal] [the specified length].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="length">The length.</param>
    /// <returns><c>true</c> if [is negative decimal] [the specified length]; otherwise, <c>false</c>.</returns>
    public static bool IsNegativeDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.NegativeDecimal, length));
    }

    /// <summary>
    ///     Determines whether [is negative decimal decimal] [the specified length].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="length">The length.</param>
    /// <returns><c>true</c> if [is negative decimal decimal] [the specified length]; otherwise, <c>false</c>.</returns>
    public static bool IsNegativeDecimalDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.NegativeDecimalDecimal, length));
    }

    /// <summary>
    ///     Determines whether the specified string is positive.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is positive; otherwise, <c>false</c>.</returns>
    public static bool IsPositive(this string str)
    {
        return Positive().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive chinese] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive chinese] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveChinese(this string str)
    {
        return PositiveChinese().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive chinese english] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive chinese english] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveChineseEnglish(this string str)
    {
        return PositiveChineseEnglish().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive chinese english underline] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive chinese english underline] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveChineseEnglishUnderline(this string str)
    {
        return PositiveChineseEnglishUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive chinese underline] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive chinese underline] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveChineseUnderline(this string str)
    {
        return PositiveChineseUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive decimal] [the specified length].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="length">The length.</param>
    /// <returns><c>true</c> if [is positive decimal] [the specified length]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveDecimal(this string str, int length = 2)
    {
        return Regex.IsMatch(str, string.Format(Const.PositiveDecimal, length));
    }

    /// <summary>
    ///     Determines whether [is positive english] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive english] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveEnglish(this string str)
    {
        return PositiveEnglish().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive english underline] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive english underline] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveEnglishUnderline(this string str)
    {
        return PositiveEnglishUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive negative] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive negative] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveNegative(this string str)
    {
        return PositiveNegative().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether [is positive underline] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is positive underline] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsPositiveUnderline(this string str)
    {
        return PositiveUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Determines whether the specified string is URL.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if the specified string is URL; otherwise, <c>false</c>.</returns>
    public static bool IsUrl(this string str)
    {
        return Url().IsMatch(str);
    }

    /// <summary>
    ///     Underlines the specified string.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>result</returns>
    public static bool Underline(this string str)
    {
        return EnglishUnderline().IsMatch(str);
    }

    /// <summary>
    ///     Chineses this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Chinese)]
    private static partial Regex Chinese();

    /// <summary>
    ///     Chineses the english.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.ChineseEnglish)]
    private static partial Regex ChineseEnglish();

    /// <summary>
    ///     Chineses the underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.ChineseUnderline)]
    private static partial Regex ChineseUnderline();

    /// <summary>
    ///     Emails this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Email)]
    private static partial Regex Email();

    /// <summary>
    ///     Englishes this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.English)]
    private static partial Regex English();

    /// <summary>
    ///     Englishes the underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.EnglishUnderline)]
    private static partial Regex EnglishUnderline();

    /// <summary>
    ///     Ips this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.IP)]
    private static partial Regex IP();

    /// <summary>
    ///     Mobiles this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Mobile)]
    private static partial Regex Mobile();

    /// <summary>
    ///     Negatives this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Negative)]
    private static partial Regex Negative();

    /// <summary>
    ///     Positives this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Positive)]
    private static partial Regex Positive();

    /// <summary>
    ///     Positives the chinese.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveChinese)]
    private static partial Regex PositiveChinese();

    /// <summary>
    ///     Positives the chinese english.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveChineseEnglish)]
    private static partial Regex PositiveChineseEnglish();

    /// <summary>
    ///     Positives the chinese english underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveChineseEnglishUnderline)]
    private static partial Regex PositiveChineseEnglishUnderline();

    /// <summary>
    ///     Positives the chinese underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveChineseUnderline)]
    private static partial Regex PositiveChineseUnderline();

    /// <summary>
    ///     Positives the english.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveEnglish)]
    private static partial Regex PositiveEnglish();

    /// <summary>
    ///     Positives the english underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveEnglishUnderline)]
    private static partial Regex PositiveEnglishUnderline();

    /// <summary>
    ///     Positives the negative.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveNegative)]
    private static partial Regex PositiveNegative();

    /// <summary>
    ///     Positives the underline.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.PositiveUnderline)]
    private static partial Regex PositiveUnderline();

    /// <summary>
    ///     URLs this instance.
    /// </summary>
    /// <returns>Regex.</returns>
    [GeneratedRegex(Const.Url)]
    private static partial Regex Url();
}
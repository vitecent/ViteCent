#region

using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class PinYinExtensions.
/// </summary>
public static class BasePinYin
{
    /// <summary>
    ///     Gets the chinese text.
    /// </summary>
    /// <param name="pinyin">The pinyin.</param>
    /// <returns>System.String.</returns>
    public static string GetChineseText(this string pinyin)
    {
        var key = pinyin.Trim().ToLower();

        foreach (var str in PinYinCode.Codes)
            if (str.StartsWith(key + " ") || str.StartsWith(key + ":"))
                return str[7..];

        return default!;
    }

    /// <summary>
    ///     Gets the initials.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string GetInitials(this string str)
    {
        str = str.Trim();
        var pingyins = new StringBuilder();

        for (var i = 0; i < str.Length; ++i)
        {
            var pinying = str[i].GetPinYin();

            if (pinying != "") pingyins.Append(pinying[0]);
        }

        return pingyins.ToString();
    }

    /// <summary>
    ///     Gets the pin yin.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string GetPinYin(this string str)
    {
        var pingyins = new StringBuilder();

        for (var i = 0; i < str.Length; ++i)
        {
            var pinying = str[i].GetPinYin();

            if (pinying != "") pingyins.Append(pinying);

            _ = pingyins.Append(' ');
        }

        return pingyins.ToString().Trim();
    }

    /// <summary>
    ///     Gets the pin yin.
    /// </summary>
    /// <param name="ch">The ch.</param>
    /// <returns>System.String.</returns>
    public static string GetPinYin(this char ch)
    {
        var hash = GetHashIndex(ch);

        for (var i = 0; i < PinYinHash.Hashes[hash].Length; ++i)
        {
            var index = PinYinHash.Hashes[hash][i];
            var pos = PinYinCode.Codes[index].IndexOf(ch);

            if (pos != -1) return PinYinCode.Codes[index].Split(":")[0];
        }

        return ch.ToString();
    }

    /// <summary>
    ///     Gets the index of the hash.
    /// </summary>
    /// <param name="ch">The ch.</param>
    /// <returns>System.Int16.</returns>
    private static short GetHashIndex(char ch)
    {
        return (short)((uint)ch % PinYinCode.Codes.Length);
    }
}
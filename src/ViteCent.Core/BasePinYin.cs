#region

using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BasePinYin
{
    /// <summary>
    /// </summary>
    /// <param name="pinyin"></param>
    /// <returns></returns>
    public static string GetChineseText(this string pinyin)
    {
        var key = pinyin.Trim().ToLower();

        foreach (var str in BasePinYinCode.Codes)
            if (str.StartsWith(key + " ") || str.StartsWith(key + ":"))
                return str[7..];

        return default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static string GetPinYin(this char ch)
    {
        var hash = GetHashIndex(ch);

        for (var i = 0; i < BasePinYinHash.Hashes[hash].Length; ++i)
        {
            var index = BasePinYinHash.Hashes[hash][i];
            var pos = BasePinYinCode.Codes[index].IndexOf(ch);

            if (pos != -1) return BasePinYinCode.Codes[index].Split(":")[0];
        }

        return ch.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="ch"></param>
    /// <returns></returns>
    private static short GetHashIndex(char ch)
    {
        return (short)((uint)ch % BasePinYinCode.Codes.Length);
    }
}
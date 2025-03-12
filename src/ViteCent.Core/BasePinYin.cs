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

        foreach (var input in BasePinYinCode.Codes)
            if (input.StartsWith(key + " ") || input.StartsWith(key + ":"))
                return input[7..];

        return default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetInitials(this string input)
    {
        input = input.Trim();
        var pingyins = new StringBuilder();

        for (var i = 0; i < input.Length; ++i)
        {
            var pinying = input[i].GetPinYin();

            if (pinying != "") pingyins.Append(pinying[0]);
        }

        return pingyins.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetPinYin(this string input)
    {
        var pingyins = new StringBuilder();

        for (var i = 0; i < input.Length; ++i)
        {
            var pinying = input[i].GetPinYin();

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
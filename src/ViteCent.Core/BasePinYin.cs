#region

using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 提供中文拼音转换和处理的工具类，支持拼音转中文、获取首字母、获取完整拼音等功能
/// </summary>
public static class BasePinYin
{
    /// <summary>
    /// 根据拼音获取对应的中文文本
    /// </summary>
    /// <param name="pinyin">输入的拼音字符串</param>
    /// <returns>返回与拼音对应的中文文本，如果未找到匹配则返回null</returns>
    public static string GetChineseText(this string pinyin)
    {
        var key = pinyin.Trim().ToLower();

        foreach (var input in BasePinYinCode.Codes)
            if (input.StartsWith(key + " ") || input.StartsWith(key + ":"))
                return input[7..];

        return default!;
    }

    /// <summary>
    /// 获取中文文本的拼音首字母
    /// </summary>
    /// <param name="input">输入的中文文本</param>
    /// <returns>返回中文文本中每个字的拼音首字母组成的字符串</returns>
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
    /// 获取中文文本的完整拼音
    /// </summary>
    /// <param name="input">输入的中文文本</param>
    /// <returns>返回中文文本对应的完整拼音，以空格分隔</returns>
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
    /// 获取单个中文字符的拼音
    /// </summary>
    /// <param name="ch">输入的中文字符</param>
    /// <returns>返回单个中文字符对应的拼音，如果不是中文字符则返回字符本身</returns>
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
    /// 获取字符的哈希索引值
    /// </summary>
    /// <param name="ch">输入的字符</param>
    /// <returns>返回字符在拼音码表中的哈希索引值</returns>
    private static short GetHashIndex(char ch)
    {
        return (short)((uint)ch % BasePinYinCode.Codes.Length);
    }
}
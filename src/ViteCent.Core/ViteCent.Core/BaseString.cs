#region

using System.Text;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core;

/// <summary>
/// 提供字符串操作和转换的工具类，包括字符串与字节数组、流的相互转换，以及字符串的隐藏、显示和格式化等功能
/// </summary>
public static class BaseString
{
    /// <summary>
    /// 将字节数组转换为流，使用UTF8编码
    /// </summary>
    /// <param name="buffer">要转换的字节数组</param>
    /// <returns>转换后的流对象</returns>
    public static Stream ByteToStream(this byte[] buffer)
    {
        return buffer.ByteToStream(Encoding.UTF8);
    }

    /// <summary>
    /// 将字节数组转换为流，使用指定编码
    /// </summary>
    /// <param name="buffer">要转换的字节数组</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>转换后的流对象</returns>
    public static Stream ByteToStream(this byte[] buffer, Encoding encoding)
    {
        return buffer.ByteToString(encoding).StringToStream(encoding);
    }

    /// <summary>
    /// 将字节数组转换为字符串，使用UTF8编码
    /// </summary>
    /// <param name="buffer">要转换的字节数组</param>
    /// <returns>转换后的字符串</returns>
    public static string ByteToString(this byte[] buffer)
    {
        return buffer.ByteToString(Encoding.UTF8);
    }

    /// <summary>
    /// 将字节数组转换为字符串，使用指定编码
    /// </summary>
    /// <param name="buffer">要转换的字节数组</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>转换后的字符串</returns>
    public static string ByteToString(this byte[] buffer, Encoding encoding)
    {
        return encoding.GetString(buffer);
    }

    /// <summary>
    /// 隐藏字符串中的部分字符，用*号替代
    /// </summary>
    /// <param name="input">要处理的字符串</param>
    /// <param name="hide">隐藏方式（开头、中间、结尾）</param>
    /// <param name="length">要隐藏的字符长度</param>
    /// <returns>处理后的字符串</returns>
    public static string HideString(this string input, HideEnum hide, int length)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        //全部隐藏
        if (length >= input.Length)
            input = "*".Repeat(input.Length);
        else
            switch (hide)
            {
                case HideEnum.Start:
                    input = $"{"*".Repeat(length)}{input[length..]}";
                    break;

                case HideEnum.Middle:
                    var start = (input.Length - length) / 2;
                    var sl = start + length;
                    input = $"{input[..start]}{"*".Repeat(length)}{input[sl..]}";
                    break;

                case HideEnum.End:
                    input = $"{input[..^length]}{"*".Repeat(length)}";
                    break;

                default:
                    throw new Exception("hide 格式错误");
            }

        return input;
    }

    /// <summary>
    /// 重复指定的字符串多次
    /// </summary>
    /// <param name="input">要重复的字符串</param>
    /// <param name="length">重复次数</param>
    /// <returns>重复后的字符串</returns>
    public static string Repeat(this string input, int length)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        var sb = new StringBuilder();

        for (var i = 0; i < length; i++) sb.Append(input);

        return sb.ToString();
    }

    /// <summary>
    /// 显示字符串中的部分字符，其余部分用*号替代
    /// </summary>
    /// <param name="input">要处理的字符串</param>
    /// <param name="hide">显示方式（开头、中间、结尾）</param>
    /// <param name="length">要显示的字符长度</param>
    /// <returns>处理后的字符串</returns>
    public static string ShowString(this string input, HideEnum hide, int length)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        switch (hide)
        {
            case HideEnum.Start:
                input = $"{input[..length]}{"*".Repeat(input.Length - length)}";
                break;

            case HideEnum.Middle:
                var start = (input.Length - length) / 2;
                input =
                    $"{"*".Repeat(start)}{input.Substring(start, length)}{"*".Repeat(input.Length - length - start)}";
                break;

            case HideEnum.End:
                input = $"{"*".Repeat(input.Length - length)}{input.Substring(input.Length - length, length)}";
                break;

            default:
                throw new Exception("hide 格式错误");
        }

        return input;
    }

    /// <summary>
    /// 将流转换为字节数组
    /// </summary>
    /// <param name="stream">要转换的流对象</param>
    /// <returns>转换后的字节数组</returns>
    public static byte[] StreamToByte(this Stream stream)
    {
        var buffer = new byte[stream.Length];
        stream.Position = 0;
        stream.ReadExactly(buffer, 0, (int)stream.Length);
        stream.Close();

        return buffer;
    }

    /// <summary>
    /// 将流转换为字符串，使用UTF8编码
    /// </summary>
    /// <param name="stream">要转换的流对象</param>
    /// <returns>转换后的字符串</returns>
    public static string StreamToString(this Stream stream)
    {
        return stream.StreamToString(Encoding.UTF8);
    }

    /// <summary>
    /// 将流转换为字符串，使用指定编码
    /// </summary>
    /// <param name="stream">要转换的流对象</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>转换后的字符串</returns>
    public static string StreamToString(this Stream stream, Encoding encoding)
    {
        return stream.StreamToByte().ByteToString(encoding);
    }

    /// <summary>
    /// 将字符串转换为字节数组，使用UTF8编码
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <returns>转换后的字节数组</returns>
    public static byte[] StringToByte(this string input)
    {
        return input.StringToByte(Encoding.UTF8);
    }

    /// <summary>
    /// 将字符串转换为字节数组，使用指定编码
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>转换后的字节数组</returns>
    public static byte[] StringToByte(this string input, Encoding encoding)
    {
        return encoding.GetBytes(input);
    }

    /// <summary>
    /// 将字符串转换为流，使用UTF8编码
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <returns>转换后的流对象</returns>
    public static Stream StringToStream(this string input)
    {
        return input.StringToStream(Encoding.UTF8);
    }

    /// <summary>
    /// 将字符串转换为流，使用指定编码
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>转换后的流对象</returns>
    public static Stream StringToStream(this string input, Encoding encoding)
    {
        return input.StringToByte(encoding).ByteToStream(encoding);
    }

    /// <summary>
    /// 将字符串转换为驼峰命名格式
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <returns>转换后的驼峰命名格式字符串</returns>
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        return input.ToCamelCase("_").ToCamelCase("-").ToCamelCase(".").ToCamelCase(" ");
    }

    /// <summary>
    /// 将字符串按指定分隔符分割并转换为驼峰命名格式
    /// </summary>
    /// <param name="input">要转换的字符串</param>
    /// <param name="split">分隔符</param>
    /// <returns>转换后的驼峰命名格式字符串</returns>
    private static string ToCamelCase(this string input, string split)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        var array = input.Split(split, StringSplitOptions.RemoveEmptyEntries).ToList();
        var sb = new StringBuilder();

        array.ForEach(x =>
        {
            if (x.Length == 1)
                sb.Append(x.ToUpper());
            else
                sb.Append(x[0].ToString().ToUpper() + x[1..]);
        });

        return sb.ToString();
    }
}
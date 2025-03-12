#region

using System.Text;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseString
{
    /// <summary>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static Stream ByteToStream(this byte[] buffer)
    {
        return buffer.ByteToStream(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static Stream ByteToStream(this byte[] buffer, Encoding encoding)
    {
        return buffer.ByteToString(encoding).StringToStream(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string ByteToString(this byte[] buffer)
    {
        return buffer.ByteToString(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string ByteToString(this byte[] buffer, Encoding encoding)
    {
        return encoding.GetString(buffer);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="hide"></param>
    /// <param name="length"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="input"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Repeat(this string input, int length)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        var sb = new StringBuilder();

        for (var i = 0; i < length; i++) sb.Append(input);

        return sb.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="hide"></param>
    /// <param name="length"></param>
    /// <returns></returns>
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
                input = $"{"*".Repeat(start)}{input.Substring(start, length)}{"*".Repeat(input.Length - length - start)}";
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
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static byte[] StreamToByte(this Stream stream)
    {
        var buffer = new byte[stream.Length];
        stream.Position = 0;
        stream.ReadExactly(buffer, 0, (int)stream.Length);
        stream.Close();

        return buffer;
    }

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string StreamToString(this Stream stream)
    {
        return stream.StreamToString(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string StreamToString(this Stream stream, Encoding encoding)
    {
        return stream.StreamToByte().ByteToString(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static byte[] StringToByte(this string input)
    {
        return input.StringToByte(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] StringToByte(this string input, Encoding encoding)
    {
        return encoding.GetBytes(input);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static Stream StringToStream(this string input)
    {
        return input.StringToStream(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static Stream StringToStream(this string input, Encoding encoding)
    {
        return input.StringToByte(encoding).ByteToStream(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string input)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        return input.ToCamelCase("_").ToCamelCase("-").ToCamelCase(".");
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    private static string ToCamelCase(this string input, string split)
    {
        if (string.IsNullOrWhiteSpace(input)) throw new Exception("input 不能为空");

        if (string.IsNullOrWhiteSpace(split)) throw new Exception("split 不能为空");

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
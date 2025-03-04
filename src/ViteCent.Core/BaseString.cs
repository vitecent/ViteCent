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
        return buffer.ByteToStream(Encoding.Default);
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
        return buffer.ByteToString(Encoding.Default);
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
    /// <param name="str"></param>
    /// <param name="hide"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string HideString(this string str, HideEnum hide, int length)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new Exception("str 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        //全部隐藏
        if (length >= str.Length)
            str = "*".Repeat(str.Length);
        else
            switch (hide)
            {
                case HideEnum.Start:
                    str = $"{"*".Repeat(length)}{str[length..]}";
                    break;

                case HideEnum.Middle:
                    var start = (str.Length - length) / 2;
                    var sl = start + length;
                    str = $"{str[..start]}{"*".Repeat(length)}{str[sl..]}";
                    break;

                case HideEnum.End:
                    str = $"{str[..^length]}{"*".Repeat(length)}";
                    break;

                default:
                    throw new Exception("hide 格式错误");
            }

        return str;
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Repeat(this string str, int length)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new Exception("str 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        var sb = new StringBuilder();

        for (var i = 0; i < length; i++) sb.Append(str);

        return sb.ToString();
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="hide"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string ShowString(this string str, HideEnum hide, int length)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new Exception("str 不能为空");

        if (length < 0) throw new Exception("length 格式错误");

        switch (hide)
        {
            case HideEnum.Start:
                str = $"{str[..length]}{"*".Repeat(str.Length - length)}";
                break;

            case HideEnum.Middle:
                var start = (str.Length - length) / 2;
                str = $"{"*".Repeat(start)}{str.Substring(start, length)}{"*".Repeat(str.Length - length - start)}";
                break;

            case HideEnum.End:
                str = $"{"*".Repeat(str.Length - length)}{str.Substring(str.Length - length, length)}";
                break;

            default:
                throw new Exception("hide 格式错误");
        }

        return str;
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
        return stream.StreamToString(Encoding.Default);
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
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] StringToByte(this string str)
    {
        return str.StringToByte(Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] StringToByte(this string str, Encoding encoding)
    {
        return encoding.GetBytes(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static Stream StringToStream(this string str)
    {
        return str.StringToStream(Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static Stream StringToStream(this string str, Encoding encoding)
    {
        return str.StringToByte(encoding).ByteToStream(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new Exception("str 不能为空");

        return str.ToCamelCase("_").ToCamelCase("-").ToCamelCase(".");
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    private static string ToCamelCase(this string str, string split)
    {
        if (string.IsNullOrWhiteSpace(str)) throw new Exception("str 不能为空");

        if (string.IsNullOrWhiteSpace(split)) throw new Exception("split 不能为空");

        var array = str.Split(split, StringSplitOptions.RemoveEmptyEntries).ToList();
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
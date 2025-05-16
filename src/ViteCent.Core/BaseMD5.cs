#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// MD5加密工具类，提供字符串的MD5哈希加密功能
/// </summary>
public static class MD5Helper
{
    /// <summary>
    /// 使用MD5算法对字符串进行加密，使用UTF8编码
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <returns>返回加密后的十六进制字符串</returns>
    public static string EncryptMD5(this string input)
    {
        return input.EncryptMD5(Encoding.UTF8);
    }

    /// <summary>
    /// 使用MD5算法对字符串进行加密，使用指定编码
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>返回加密后的十六进制字符串</returns>
    public static string EncryptMD5(this string input, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = MD5.Create();
        var hash = provider.ComputeHash(buffer);

        return BitConverter.ToString(hash).Replace("-", "");
    }
}
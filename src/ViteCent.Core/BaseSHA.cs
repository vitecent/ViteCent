#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// SHA1加密工具类，提供字符串的SHA1哈希加密功能
/// </summary>
public static class BaseSHA
{
    /// <summary>
    /// 使用SHA1算法对字符串进行加密，使用UTF8编码
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <returns>返回加密后的十六进制字符串</returns>
    public static string EncryptSHA(this string input)
    {
        return input.EncryptSHA(Encoding.UTF8);
    }

    /// <summary>
    /// 使用SHA1算法对字符串进行加密，使用指定编码
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>返回加密后的十六进制字符串</returns>
    public static string EncryptSHA(this string input, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = SHA1.Create();
        buffer = provider.ComputeHash(buffer);
        var result = new StringBuilder();

        foreach (var temp in buffer) result.AppendFormat("{0:x2}", temp);

        return result.ToString();
    }
}
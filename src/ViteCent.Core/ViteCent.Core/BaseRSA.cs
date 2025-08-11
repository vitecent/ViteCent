#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// RSA加密工具类，提供字符串的RSA加密和解密功能
/// </summary>
public static class RSAHelper
{
    /// <summary>
    /// 使用RSA算法对字符串进行解密，使用UTF8编码
    /// </summary>
    /// <param name="input">要解密的Base64字符串</param>
    /// <param name="key">RSA私钥的Base64字符串</param>
    /// <returns>返回解密后的原始字符串</returns>
    public static string DecryptRSA(this string input, string key)
    {
        return input.DecryptRSA(key, Encoding.UTF8);
    }

    /// <summary>
    /// 使用RSA算法对字符串进行解密，使用指定编码
    /// </summary>
    /// <param name="input">要解密的Base64字符串</param>
    /// <param name="key">RSA私钥的Base64字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>返回解密后的原始字符串</returns>
    public static string DecryptRSA(this string input, string key, Encoding encoding)
    {
        var strBuffer = input.DecryptBase64();
        var keyBuffer = key.DecryptBase64();
        var provider = new RSACryptoServiceProvider();
        provider.ImportCspBlob(keyBuffer);
        var result = provider.Decrypt(strBuffer, false);

        return result.ByteToString(encoding);
    }

    /// <summary>
    /// 使用RSA算法对字符串进行加密，使用UTF8编码，并生成RSA密钥对
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <param name="key">输出参数，返回RSA私钥的Base64字符串</param>
    /// <returns>返回加密后的Base64字符串</returns>
    public static string EncryptRSA(this string input, out string key)
    {
        return input.EncryptRSA(out key, Encoding.UTF8);
    }

    /// <summary>
    /// 使用RSA算法对字符串进行加密，使用指定编码，并生成RSA密钥对
    /// </summary>
    /// <param name="input">要加密的字符串</param>
    /// <param name="key">输出参数，返回RSA私钥的Base64字符串</param>
    /// <param name="encoding">指定的字符编码</param>
    /// <returns>返回加密后的Base64字符串</returns>
    public static string EncryptRSA(this string input, out string key, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = new RSACryptoServiceProvider();
        var result = provider.Encrypt(buffer, false);
        key = provider.ExportCspBlob(true).EncryptBase64();

        return result.EncryptBase64();
    }
}
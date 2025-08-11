#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// DES加密解密工具类，提供字符串的DES加密和解密功能
/// </summary>
public static class DESHelper
{
    /// <summary>
    /// 使用DES算法解密字符串，使用默认的UTF8编码、ECB模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <param name="key">解密密钥</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptDES(this string input, string key)
    {
        return input.DecryptDES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法解密字符串，使用指定编码、ECB模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptDES(this string input, string key, Encoding encoding)
    {
        return input.DecryptDES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法解密字符串，使用指定编码、加密模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的运算模式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptDES(this string input, string key, Encoding encoding, CipherMode mode)
    {
        return input.DecryptDES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法解密字符串，可以指定编码、加密模式和填充模式
    /// </summary>
    /// <param name="input">需要解密的字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的运算模式</param>
    /// <param name="padding">填充模式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptDES(this string input, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var iv = keyBuffer;
        var strBuffter = input.DecryptBase64();

        var provider = DES.Create();
        provider.Mode = mode;
        provider.Padding = padding;
        var result = new MemoryStream();

        var copy = new CryptoStream(result, provider.CreateDecryptor(keyBuffer, iv), CryptoStreamMode.Write);
        copy.Write(strBuffter, 0, strBuffter.Length);
        copy.FlushFinalBlock();

        return result.ToArray().ByteToString(encoding);
    }

    /// <summary>
    /// 使用DES算法加密字符串，使用默认的UTF8编码、ECB模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要加密的原始字符串</param>
    /// <param name="key">加密密钥</param>
    /// <returns>加密后的Base64字符串</returns>
    public static string EncryptDES(this string input, string key)
    {
        return input.EncryptDES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法加密字符串，使用指定编码、ECB模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要加密的原始字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <returns>加密后的Base64字符串</returns>
    public static string EncryptDES(this string input, string key, Encoding encoding)
    {
        return input.EncryptDES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法加密字符串，使用指定编码、加密模式和PKCS7填充
    /// </summary>
    /// <param name="input">需要加密的原始字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的运算模式</param>
    /// <returns>加密后的Base64字符串</returns>
    public static string EncryptDES(this string input, string key, Encoding encoding, CipherMode mode)
    {
        return input.EncryptDES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用DES算法加密字符串，可以指定编码、加密模式和填充模式
    /// </summary>
    /// <param name="input">需要加密的原始字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的运算模式</param>
    /// <param name="padding">填充模式</param>
    /// <returns>加密后的Base64字符串</returns>
    public static string EncryptDES(this string input, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var iv = keyBuffer;
        var strBuffter = input.StringToByte(encoding);

        var provider = DES.Create();
        provider.Mode = mode;
        provider.Padding = padding;

        var result = new MemoryStream();

        var copy = new CryptoStream(result, provider.CreateEncryptor(keyBuffer, iv), CryptoStreamMode.Write);
        copy.Write(strBuffter, 0, strBuffter.Length);
        copy.FlushFinalBlock();

        return result.ToArray().EncryptBase64();
    }
}
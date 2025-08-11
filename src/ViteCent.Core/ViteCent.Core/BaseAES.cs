#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// AES加解密工具类，提供字符串的AES加密和解密功能
/// </summary>
public static class BaseAES
{
    /// <summary>
    /// 使用指定密钥对AES加密的字符串进行解密
    /// </summary>
    /// <param name="input">需要解密的AES加密字符串</param>
    /// <param name="key">解密密钥</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptAES(this string input, string key)
    {
        return input.DecryptAES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥和编码方式对AES加密的字符串进行解密
    /// </summary>
    /// <param name="input">需要解密的AES加密字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptAES(this string input, string key, Encoding encoding)
    {
        return input.DecryptAES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥、编码方式和加密模式对AES加密的字符串进行解密
    /// </summary>
    /// <param name="input">需要解密的AES加密字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的模式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptAES(this string input, string key, Encoding encoding, CipherMode mode)
    {
        return input.DecryptAES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥、编码方式、加密模式和填充模式对AES加密的字符串进行解密
    /// </summary>
    /// <param name="input">需要解密的AES加密字符串</param>
    /// <param name="key">解密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的模式</param>
    /// <param name="padding">填充模式</param>
    /// <returns>解密后的原始字符串</returns>
    public static string DecryptAES(this string input, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var str64 = input.DecryptBase64();

        var provider = Aes.Create();
        provider.Key = keyBuffer;
        provider.Mode = mode;
        provider.Padding = padding;

        var from = provider.CreateDecryptor();
        var result = from.TransformFinalBlock(str64, 0, str64.Length);

        return result.ByteToString(encoding);
    }

    /// <summary>
    /// 使用指定密钥对字符串进行AES加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="key">加密密钥</param>
    /// <returns>AES加密后的字符串</returns>
    public static string EncryptAES(this string input, string key)
    {
        return input.EncryptAES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥和编码方式对字符串进行AES加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <returns>AES加密后的字符串</returns>
    public static string EncryptAES(this string input, string key, Encoding encoding)
    {
        return input.EncryptAES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥、编码方式和加密模式对字符串进行AES加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的模式</param>
    /// <returns>AES加密后的字符串</returns>
    public static string EncryptAES(this string input, string key, Encoding encoding, CipherMode mode)
    {
        return input.EncryptAES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// 使用指定密钥、编码方式、加密模式和填充模式对字符串进行AES加密
    /// </summary>
    /// <param name="input">需要加密的字符串</param>
    /// <param name="key">加密密钥</param>
    /// <param name="encoding">字符编码方式</param>
    /// <param name="mode">加密器的模式</param>
    /// <param name="padding">填充模式</param>
    /// <returns>AES加密后的字符串</returns>
    public static string EncryptAES(this string input, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = encoding.GetBytes(key);
        var strBuffer = encoding.GetBytes(input);

        var provider = Aes.Create();
        provider.Key = keyBuffer;
        provider.Mode = mode;
        provider.Padding = padding;

        var from = provider.CreateEncryptor();
        var result = from.TransformFinalBlock(strBuffer, 0, strBuffer.Length);

        return result.EncryptBase64();
    }
}
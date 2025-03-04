#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseAES
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DecryptAES(this string str, string key)
    {
        return str.DecryptAES(key, Encoding.Default, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string DecryptAES(this string str, string key, Encoding encoding)
    {
        return str.DecryptAES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static string DecryptAES(this string str, string key, Encoding encoding, CipherMode mode)
    {
        return str.DecryptAES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static string DecryptAES(this string str, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var str64 = str.DecryptBase64();

        var provider = Aes.Create();
        provider.Key = keyBuffer;
        provider.Mode = mode;
        provider.Padding = padding;

        var from = provider.CreateDecryptor();
        var result = from.TransformFinalBlock(str64, 0, str64.Length);

        return result.ByteToString(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptAES(this string str, string key)
    {
        return str.EncryptAES(key, Encoding.Default, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptAES(this string str, string key, Encoding encoding)
    {
        return str.EncryptAES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static string EncryptAES(this string str, string key, Encoding encoding, CipherMode mode)
    {
        return str.EncryptAES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static string EncryptAES(this string str, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = encoding.GetBytes(key);
        var strBuffer = encoding.GetBytes(str);

        var provider = Aes.Create();
        provider.Key = keyBuffer;
        provider.Mode = mode;
        provider.Padding = padding;

        var from = provider.CreateEncryptor();
        var result = from.TransformFinalBlock(strBuffer, 0, strBuffer.Length);

        return result.EncryptBase64();
    }
}
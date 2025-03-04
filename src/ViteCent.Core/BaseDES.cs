#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class DESHelper
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DecryptDES(this string str, string key)
    {
        return str.DecryptDES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string DecryptDES(this string str, string key, Encoding encoding)
    {
        return str.DecryptDES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static string DecryptDES(this string str, string key, Encoding encoding, CipherMode mode)
    {
        return str.DecryptDES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static string DecryptDES(this string str, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var iv = keyBuffer;
        var strBuffter = str.DecryptBase64();

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
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptDES(this string str, string key)
    {
        return str.EncryptDES(key, Encoding.UTF8, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptDES(this string str, string key, Encoding encoding)
    {
        return str.EncryptDES(key, encoding, CipherMode.ECB, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static string EncryptDES(this string str, string key, Encoding encoding, CipherMode mode)
    {
        return str.EncryptDES(key, encoding, mode, PaddingMode.PKCS7);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <param name="mode"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public static string EncryptDES(this string str, string key, Encoding encoding, CipherMode mode,
        PaddingMode padding)
    {
        var keyBuffer = key.StringToByte(encoding);
        var iv = keyBuffer;
        var strBuffter = str.StringToByte(encoding);

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
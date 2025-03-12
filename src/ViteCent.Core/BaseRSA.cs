#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class RSAHelper
{
    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DecryptRSA(this string input, string key)
    {
        return input.DecryptRSA(key, Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptRSA(this string input, out string key)
    {
        return input.EncryptRSA(out key, Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptRSA(this string input, out string key, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = new RSACryptoServiceProvider();
        var result = provider.Encrypt(buffer, false);
        key = provider.ExportCspBlob(true).EncryptBase64();

        return result.EncryptBase64();
    }
}
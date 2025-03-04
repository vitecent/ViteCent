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
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DecryptRSA(this string str, string key)
    {
        return str.DecryptRSA(key, Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string DecryptRSA(this string str, string key, Encoding encoding)
    {
        var strBuffer = str.DecryptBase64();
        var keyBuffer = key.DecryptBase64();
        var provider = new RSACryptoServiceProvider();
        provider.ImportCspBlob(keyBuffer);
        var result = provider.Decrypt(strBuffer, false);

        return result.ByteToString(encoding);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string EncryptRSA(this string str, out string key)
    {
        return str.EncryptRSA(out key, Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="key"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptRSA(this string str, out string key, Encoding encoding)
    {
        var buffer = str.StringToByte(encoding);
        var provider = new RSACryptoServiceProvider();
        var result = provider.Encrypt(buffer, false);
        key = provider.ExportCspBlob(true).EncryptBase64();

        return result.EncryptBase64();
    }
}
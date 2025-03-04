#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class MD5Helper
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string EncryptMD5(this string str)
    {
        return str.EncryptMD5(Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptMD5(this string str, Encoding encoding)
    {
        var buffer = str.StringToByte(encoding);
        var provider = MD5.Create();
        var hash = provider.ComputeHash(buffer);

        return BitConverter.ToString(hash).Replace("-", "");
    }
}
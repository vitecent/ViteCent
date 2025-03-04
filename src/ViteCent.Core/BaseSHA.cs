#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseSHA
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string EncryptSHA(this string str)
    {
        return str.EncryptSHA(Encoding.Default);
    }

    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptSHA(this string str, Encoding encoding)
    {
        var buffer = str.StringToByte(encoding);
        var provider = SHA1.Create();
        buffer = provider.ComputeHash(buffer);
        var result = new StringBuilder();

        foreach (var temp in buffer) result.AppendFormat("{0:x2}", temp);

        return result.ToString();
    }
}
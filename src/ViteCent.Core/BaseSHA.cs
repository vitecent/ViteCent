#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class SHAExtensions.
/// </summary>
public static class BaseSHA
{
    /// <summary>
    ///     Encrypts the sha.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string EncryptSHA(this string str)
    {
        return str.EncryptSHA(Encoding.Default);
    }

    /// <summary>
    ///     Encrypts the sha.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>System.String.</returns>
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
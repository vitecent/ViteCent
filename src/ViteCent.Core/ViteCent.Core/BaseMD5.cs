#region

using System.Security.Cryptography;
using System.Text;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class MD5Helper.
/// </summary>
public static class MD5Helper
{
    /// <summary>
    ///     Encrypts the m d5.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string EncryptMD5(this string str)
    {
        return str.EncryptMD5(Encoding.Default);
    }

    /// <summary>
    ///     Encrypts the m d5.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <param name="encoding">The encoding.</param>
    /// <returns>System.String.</returns>
    public static string EncryptMD5(this string str, Encoding encoding)
    {
        var buffer = str.StringToByte(encoding);
        var provider = MD5.Create();
        var hash = provider.ComputeHash(buffer);

        return BitConverter.ToString(hash).Replace("-", "");
    }
}
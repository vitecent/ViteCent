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
    /// <param name="input"></param>
    /// <returns></returns>
    public static string EncryptMD5(this string input)
    {
        return input.EncryptMD5(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptMD5(this string input, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = MD5.Create();
        var hash = provider.ComputeHash(buffer);

        return BitConverter.ToString(hash).Replace("-", "");
    }
}
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
    /// <param name="input"></param>
    /// <returns></returns>
    public static string EncryptSHA(this string input)
    {
        return input.EncryptSHA(Encoding.UTF8);
    }

    /// <summary>
    /// </summary>
    /// <param name="input"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static string EncryptSHA(this string input, Encoding encoding)
    {
        var buffer = input.StringToByte(encoding);
        var provider = SHA1.Create();
        buffer = provider.ComputeHash(buffer);
        var result = new StringBuilder();

        foreach (var temp in buffer) result.AppendFormat("{0:x2}", temp);

        return result.ToString();
    }
}
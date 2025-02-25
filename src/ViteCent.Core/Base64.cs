namespace ViteCent.Core;

/// <summary>
///     Class Base64Extensions.
/// </summary>
public static class Base64
{
    /// <summary>
    ///     Decrypts the base64.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.Byte[].</returns>
    public static byte[] DecryptBase64(this string str)
    {
        return Convert.FromBase64String(str);
    }

    /// <summary>
    ///     Encrypts the base64.
    /// </summary>
    /// <param name="buffer">The buffer.</param>
    /// <returns>System.String.</returns>
    public static string EncryptBase64(this byte[] buffer)
    {
        return Convert.ToBase64String(buffer);
    }
}
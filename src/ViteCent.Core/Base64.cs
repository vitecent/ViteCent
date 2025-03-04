namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class Base64
{
    /// <summary>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] DecryptBase64(this string str)
    {
        return Convert.FromBase64String(str);
    }

    /// <summary>
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public static string EncryptBase64(this byte[] buffer)
    {
        return Convert.ToBase64String(buffer);
    }
}
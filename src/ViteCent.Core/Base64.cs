namespace ViteCent.Core;

/// <summary>
/// Base64编解码工具类，提供字符串与字节数组之间的Base64转换功能
/// </summary>
public static class Base64
{
    /// <summary>
    /// 将Base64编码的字符串解码为字节数组
    /// </summary>
    /// <param name="input">需要解码的Base64字符串</param>
    /// <returns>解码后的字节数组</returns>
    public static byte[] DecryptBase64(this string input)
    {
        return Convert.FromBase64String(input);
    }

    /// <summary>
    /// 将字节数组编码为Base64字符串
    /// </summary>
    /// <param name="buffer">需要编码的字节数组</param>
    /// <returns>Base64编码后的字符串</returns>
    public static string EncryptBase64(this byte[] buffer)
    {
        return Convert.ToBase64String(buffer);
    }
}
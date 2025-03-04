namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class Const
{
    /// <summary>
    /// </summary>
    public const string Check = "/Check";

    /// <summary>
    /// </summary>
    public const string Chinese = @"^[\u4e00-\u9fa5]+?$";

    /// <summary>
    /// </summary>
    public const string ChineseEnglish = @"^[\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    /// </summary>
    public const string ChineseUnderline = @"^[\u4e00-\u9fa5_]+$";

    /// <summary>
    /// </summary>
    public const string Decimal = @"^\.\d{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

    /// <summary>
    /// </summary>
    public const string English = @"^[A-Za-z]+?$";

    /// <summary>
    /// </summary>
    public const string EnglishUnderline = @"^[A-Za-z_]+$";

    /// <summary>
    /// </summary>
    public const string IP = @"^\d+\.\d+\.\d+\.\d+$";

    /// <summary>
    /// </summary>
    public const string Mobile = @"^1[3-8]\d{9}";

    /// <summary>
    /// </summary>
    public const string Negative = @"-\d+$";

    /// <summary>
    /// </summary>
    public const string NegativeDecimal = @"^-\d+\.\d{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string NegativeDecimalDecimal = @"^-?\d+\.\d{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string Positive = @"^\d+$";

    /// <summary>
    /// </summary>
    public const string PositiveChinese = @"^[\d\u4e00-\u9fa5]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseEnglish = @"^[\d\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseEnglishUnderline = @"^[\d\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseUnderline = @"^[\d\u4e00-\u9fa5_]+$";

    /// <summary>
    /// </summary>
    public const string PositiveDecimal = @"^\d+\.\d{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string PositiveEnglish = @"^[\dA-Za-z]+$";

    /// <summary>
    /// </summary>
    public const string PositiveEnglishUnderline = @"^\w+$";

    /// <summary>
    /// </summary>
    public const string PositiveNegative = @"^-?\d+$";

    /// <summary>
    /// </summary>
    public const string PositiveUnderline = @"^[\d_]+$";

    /// <summary>
    /// </summary>
    public const string RegistServices = "RegistServices";

    /// <summary>
    /// </summary>
    public const string Salf = "!&5s@1#3*$Rd";

    /// <summary>
    /// </summary>
    public const string Token = "Authorization";

    /// <summary>
    /// </summary>
    public const string TraceingId = "User-TraceingId";

    /// <summary>
    /// </summary>
    public const string Url = @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]$";
}
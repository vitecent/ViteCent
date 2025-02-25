namespace ViteCent.Core.Data;

/// <summary>
///     Class Const.
/// </summary>
public class Const
{
    /// <summary>
    ///     The Traceing key
    /// </summary>
    public const string Check = "/Check";

    /// <summary>
    ///     The chinese
    /// </summary>
    public const string Chinese = @"^[\u4e00-\u9fa5]+?$";

    /// <summary>
    ///     The chinese english
    /// </summary>
    public const string ChineseEnglish = @"^[\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    ///     The chinese underline
    /// </summary>
    public const string ChineseUnderline = @"^[\u4e00-\u9fa5_]+$";

    /// <summary>
    ///     The decimal
    /// </summary>
    public const string Decimal = @"^\.\d{1,{0}}$";

    /// <summary>
    ///     The email
    /// </summary>
    public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

    /// <summary>
    ///     The english
    /// </summary>
    public const string English = @"^[A-Za-z]+?$";

    /// <summary>
    ///     The english underline
    /// </summary>
    public const string EnglishUnderline = @"^[A-Za-z_]+$";

    /// <summary>
    ///     The ip
    /// </summary>
    public const string IP = @"^\d+\.\d+\.\d+\.\d+$";

    /// <summary>
    ///     The mobile
    /// </summary>
    public const string Mobile = @"^1[3-8]\d{9}";

    /// <summary>
    ///     The negative
    /// </summary>
    public const string Negative = @"-\d+$";

    /// <summary>
    ///     The negative decimal
    /// </summary>
    public const string NegativeDecimal = @"^-\d+\.\d{1,{0}}$";

    /// <summary>
    ///     The negative decimal decimal
    /// </summary>
    public const string NegativeDecimalDecimal = @"^-?\d+\.\d{1,{0}}$";

    /// <summary>
    ///     The positive
    /// </summary>
    public const string Positive = @"^\d+$";

    /// <summary>
    ///     The positive chinese
    /// </summary>
    public const string PositiveChinese = @"^[\d\u4e00-\u9fa5]+$";

    /// <summary>
    ///     The positive chinese english
    /// </summary>
    public const string PositiveChineseEnglish = @"^[\d\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    ///     The positive chinese english underline
    /// </summary>
    public const string PositiveChineseEnglishUnderline = @"^[\d\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    ///     The positive chinese underline
    /// </summary>
    public const string PositiveChineseUnderline = @"^[\d\u4e00-\u9fa5_]+$";

    /// <summary>
    ///     The positive decimal
    /// </summary>
    public const string PositiveDecimal = @"^\d+\.\d{1,{0}}$";

    /// <summary>
    ///     The positive english
    /// </summary>
    public const string PositiveEnglish = @"^[\dA-Za-z]+$";

    /// <summary>
    ///     The positive english underline
    /// </summary>
    public const string PositiveEnglishUnderline = @"^\w+$";

    /// <summary>
    ///     The positive negative
    /// </summary>
    public const string PositiveNegative = @"^-?\d+$";

    /// <summary>
    ///     The positive underline
    /// </summary>
    public const string PositiveUnderline = @"^[\d_]+$";

    /// <summary>
    ///     The Services
    /// </summary>
    public const string RegistServices = "RegistServices";

    /// <summary>
    ///     The salf
    /// </summary>
    public const string Salf = "!&5s@1#3*$Rd";

    /// <summary>
    ///     The token
    /// </summary>
    public const string Token = "Authorization";

    /// <summary>
    ///     The Traceing key
    /// </summary>
    public const string TraceingId = "User-TraceingId";

    /// <summary>
    ///     The URL
    /// </summary>
    public const string Url = @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]$";
}
namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class BaseConst
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
    public const string ChineseEnglishUnderline = @"^[\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    /// </summary>
    public const string ChineseUnderline = @"^[\u4e00-\u9fa5_]+$";

    /// <summary>
    /// </summary>
    public const string Decimal = @"^\.0-9{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string DefaultPassword = "123qwe";

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
    public const string IP = @"^0-9+\.0-9+\.0-9+\.0-9+$";

    /// <summary>
    /// </summary>
    public const string Mobile = @"^1[3-8]0-9{9}";

    /// <summary>
    /// </summary>
    public const string Negative = @"-0-9+$";

    /// <summary>
    /// </summary>
    public const string NegativeDecimal = @"^-?0-9+\.0-9{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string Password = @"^[A-Za-z0-9!@#$%^&*()_+\-=<>?]+?$";

    /// <summary>
    /// </summary>
    public const string Positive = @"^0-9+$";

    /// <summary>
    /// </summary>
    public const string PositiveChinese = @"^[0-9\u4e00-\u9fa5]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseEnglish = @"^[0-9\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseEnglishUnderline = @"^[0-9\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    /// </summary>
    public const string PositiveChineseUnderline = @"^[0-9\u4e00-\u9fa5_]+$";

    /// <summary>
    /// </summary>
    public const string PositiveDecimal = @"^0-9+\.0-9{1,{0}}$";

    /// <summary>
    /// </summary>
    public const string PositiveEnglish = @"^[0-9A-Za-z]+$";

    /// <summary>
    /// </summary>
    public const string PositiveEnglishUnderline = @"^\w+$";

    /// <summary>
    /// </summary>
    public const string PositiveNegative = @"^-?0-9+$";

    /// <summary>
    /// </summary>
    public const string PositiveUnderline = @"^[0-9_]+$";

    /// <summary>
    /// </summary>
    public const string RegistServices = "RegistServices";

    /// <summary>
    /// </summary>
    public const string Salf = "!&5s@1#3*$Rd";

    /// <summary>
    /// </summary>
    public const string Time = @"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$";

    /// <summary>
    /// </summary>
    public const string Token = "Authorization";

    /// <summary>
    /// </summary>
    public const string TraceingId = "User-TraceingId";

    /// <summary>
    /// </summary>
    public const string Url = @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]$";

    /// <summary>
    /// </summary>
    public const string UserName = @"^[A-Za-z][A-Za-z0-9]*$";
}
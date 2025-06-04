namespace ViteCent.Core.Data;

/// <summary>
/// 基础常量定义类，包含系统常用的正则表达式和配置常量
/// </summary>
public class BaseConst
{
    /// <summary>
    /// 健康检查路由路径
    /// </summary>
    public const string Check = "/Check";

    /// <summary>
    /// 纯中文字符匹配正则表达式，只允许中文字符
    /// </summary>
    public const string Chinese = @"^[\u4e00-\u9fa5]+?$";

    /// <summary>
    /// 中英文混合匹配正则表达式，允许中文和英文字母
    /// </summary>
    public const string ChineseEnglish = @"^[\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    /// 中英文及下划线匹配正则表达式，允许中文、英文字母和下划线
    /// </summary>
    public const string ChineseEnglishUnderline = @"^[\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    /// 中文及下划线匹配正则表达式，允许中文和下划线
    /// </summary>
    public const string ChineseUnderline = @"^[\u4e00-\u9fa5_]+$";

    /// <summary>
    /// 小数匹配正则表达式，用于验证小数格式
    /// </summary>
    public const string Decimal = @"^\.0-9{1,{0}}$";

    /// <summary>
    /// 系统默认密码
    /// </summary>
    public const string DefaultPassword = "123qwe";

    /// <summary>
    /// 电子邮箱格式匹配正则表达式
    /// </summary>
    public const string Email = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

    /// <summary>
    /// 纯英文字母匹配正则表达式，只允许英文字母
    /// </summary>
    public const string English = @"^[A-Za-z]+?$";

    /// <summary>
    /// 英文及下划线匹配正则表达式，允许英文字母和下划线
    /// </summary>
    public const string EnglishUnderline = @"^[A-Za-z_]+$";

    /// <summary>
    /// IP地址格式匹配正则表达式
    /// </summary>
    public const string IP = @"^0-9+\.0-9+\.0-9+\.0-9+$";

    /// <summary>
    /// 手机号码格式匹配正则表达式，支持国内手机号码格式
    /// </summary>
    public const string Mobile = @"^1[3-8]0-9{9}";

    /// <summary>
    /// 负数匹配正则表达式，用于验证负整数
    /// </summary>
    public const string Negative = @"-0-9+$";

    /// <summary>
    /// 负数小数匹配正则表达式，用于验证负小数
    /// </summary>
    public const string NegativeDecimal = @"^-?0-9+\.0-9{1,{0}}$";

    /// <summary>
    /// 密码格式匹配正则表达式，允许英文字母、数字和特殊字符
    /// </summary>
    public const string Password = @"^[A-Za-z0-9!@#$%^&*()_+\-=<>?]+?$";

    /// <summary>
    /// 正整数匹配正则表达式，只允许正整数
    /// </summary>
    public const string Positive = @"^0-9+$";

    /// <summary>
    /// 数字和中文混合匹配正则表达式，允许数字和中文字符
    /// </summary>
    public const string PositiveChinese = @"^[0-9\u4e00-\u9fa5]+$";

    /// <summary>
    /// 数字、中文和英文混合匹配正则表达式，允许数字、中文和英文字母
    /// </summary>
    public const string PositiveChineseEnglish = @"^[0-9\u4e00-\u9fa5A-Za-z]+$";

    /// <summary>
    /// 数字、中文、英文和下划线混合匹配正则表达式
    /// </summary>
    public const string PositiveChineseEnglishUnderline = @"^[0-9\u4e00-\u9fa5A-Za-z_]+$";

    /// <summary>
    /// 数字、中文和下划线混合匹配正则表达式
    /// </summary>
    public const string PositiveChineseUnderline = @"^[0-9\u4e00-\u9fa5_]+$";

    /// <summary>
    /// 正小数匹配正则表达式，用于验证正小数
    /// </summary>
    public const string PositiveDecimal = @"^0-9+\.0-9{1,{0}}$";

    /// <summary>
    /// 数字和英文混合匹配正则表达式，允许数字和英文字母
    /// </summary>
    public const string PositiveEnglish = @"^[0-9A-Za-z]+$";

    /// <summary>
    /// 数字、字母和下划线混合匹配正则表达式（等同于\w）
    /// </summary>
    public const string PositiveEnglishUnderline = @"^\w+$";

    /// <summary>
    /// 整数匹配正则表达式，允许正数和负数
    /// </summary>
    public const string PositiveNegative = @"^-?0-9+$";

    /// <summary>
    /// 数字和下划线混合匹配正则表达式
    /// </summary>
    public const string PositiveUnderline = @"^[0-9_]+$";

    /// <summary>
    /// 服务注册标识符
    /// </summary>
    public const string RegistServices = "RegistServices";

    /// <summary>
    /// 系统加密盐值
    /// </summary>
    public const string Salf = "!&5s@1#3*$Rd";

    /// <summary>
    /// 24小时制时间格式匹配正则表达式（HH:mm:ss）
    /// </summary>
    public const string Time = @"^(?:[01]\d|2[0-3]):[0-5]\d:[0-5]\d$";

    /// <summary>
    /// JWT认证请求头名称
    /// </summary>
    public const string Token = "Authorization";

    /// <summary>
    /// 用户请求追踪标识请求头名称
    /// </summary>
    public const string TraceingId = "User-TraceingId";

    /// <summary>
    /// URL格式匹配正则表达式，支持http、https、ftp和file协议
    /// </summary>
    public const string Url = @"^(https?|ftp|file)://[-A-Za-z0-9+&@#/%?=~_|!:,.;]+[-A-Za-z0-9+&@#/%=~_|]$";

    /// <summary>
    /// 用户名格式匹配正则表达式，必须以字母开头，后面允许字母和数字
    /// </summary>
    public const string UserName = @"^[A-Za-z][A-Za-z0-9]*$";
}
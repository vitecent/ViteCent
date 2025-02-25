namespace ViteCent.Core;

/// <summary>
///     Class IdCardHelper.
/// </summary>
public static class IdCardHelper
{
    /// <summary>
    ///     Gets the identifier card birthday.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string GetIdCardBirthday(this string str)
    {
        if (!str.IsIdCard()) return string.Empty;

        if (str.Length == 18)
            return str.GetIdCardBirthday18();
        if (str.Length == 15)
            return str.GetIdCardBirthday15();
        throw new Exception("身份证号格式错误");
    }

    /// <summary>
    ///     Gets the identifier card birthday15.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string GetIdCardBirthday15(this string str)
    {
        return str.Substring(6, 6).Insert(4, "-").Insert(2, "-");
    }

    /// <summary>
    ///     Gets the identifier card birthday18.
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns>System.String.</returns>
    public static string GetIdCardBirthday18(this string str)
    {
        return str.Substring(6, 8).Insert(6, "-").Insert(4, "-");
    }

    /// <summary>
    ///     Determines whether [is identifier card] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is identifier card] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsIdCard(this string str)
    {
        if (string.IsNullOrWhiteSpace(str)) return false;

        if (str.Length == 18)
            return str.IsIdCard18();
        if (str.Length == 15)
            return str.IsIdCard15();
        return false;
    }

    /// <summary>
    ///     Determines whether [is identifier card15] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is identifier card15] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsIdCard15(this string str)
    {
        //数字验证
        if (long.TryParse(str, out var n) == false || n < Math.Pow(10, 14)) return false;

        //省份验证
        var address =
            "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(str.Remove(2), StringComparison.CurrentCulture)) return false;

        //生日验证
        var birth = str.Substring(6, 6).Insert(4, "-").Insert(2, "-");
        if (DateTime.TryParse(birth, out _) == false) return false;

        return true;
    }

    /// <summary>
    ///     Determines whether [is identifier card18] [the specified string].
    /// </summary>
    /// <param name="str">The string.</param>
    /// <returns><c>true</c> if [is identifier card18] [the specified string]; otherwise, <c>false</c>.</returns>
    public static bool IsIdCard18(this string str)
    {
        //数字验证
        if (long.TryParse(str.Remove(17), out var x) == false || x < Math.Pow(10, 16) ||
            long.TryParse(str.Replace('x', '0').Replace('X', '0'), out _) == false) return false;

        //省份验证
        var address =
            "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
        if (!address.Contains(str.Remove(2), StringComparison.CurrentCulture)) return false;

        //生日验证
        var birth = str.Substring(6, 8).Insert(6, "-").Insert(4, "-");
        if (DateTime.TryParse(birth, out _) == false) return false;

        //校验码验证
        var arrVarifyCode = "1,0,x,9,8,7,6,5,4,3,2".Split(',');
        var Wi = "7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2".Split(',');
        var Ai = str.Remove(17).ToCharArray();
        var sum = 0;
        for (var i = 0; i < 17; i++) sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());

        Math.DivRem(sum, 11, out var y);

        if (!arrVarifyCode[y].Equals(str.Substring(17, 1), StringComparison.CurrentCultureIgnoreCase)) return false;

        return true;
    }
}
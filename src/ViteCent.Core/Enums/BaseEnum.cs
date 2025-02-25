#region

using System.ComponentModel;

#endregion

namespace ViteCent.Core.Enums;

/// <summary>
///     EnumHelper
/// </summary>
public static class BaseEnum
{
    /// <summary>
    ///     Gets the description.
    /// </summary>
    /// <param name="enums">The enums.</param>
    /// <returns>System.String.</returns>
    public static string GetDescription(this Enum enums)
    {
        var strValue = enums.ToString();
        var fieldinfo = enums.GetType().GetField(strValue);

        if (fieldinfo == null) return strValue;

        var objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (objs == null || objs.Length == 0) return strValue;

        var da = (DescriptionAttribute)objs[0];

        return da.Description;
    }

    /// <summary>
    ///     Gets the description by value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">The value.</param>
    /// <returns>System.String.</returns>
    public static string GetDescriptionByValue<T>(int value)
    {
        foreach (var item in Enum.GetValues(typeof(T)))
        {
            Enum.TryParse(typeof(T), item.ToString(), out var obj);
            if (obj != null)
                if (obj is Enum enumValue)
                    if (value == Convert.ToInt32(enumValue))
                        return enumValue.GetDescription();
        }

        return default!;
    }
}
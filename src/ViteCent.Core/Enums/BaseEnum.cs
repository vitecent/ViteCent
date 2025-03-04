#region

using System.ComponentModel;

#endregion

namespace ViteCent.Core.Enums;

/// <summary>
/// </summary>
public static class BaseEnum
{
    /// <summary>
    /// </summary>
    /// <param name="enums"></param>
    /// <returns></returns>
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
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
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
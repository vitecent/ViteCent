#region

using System.ComponentModel;

#endregion

namespace ViteCent.Core.Enums;

/// <summary>
/// 枚举工具类，提供枚举类型的扩展方法，用于获取枚举成员的描述信息
/// </summary>
public static class BaseEnum
{
    /// <summary>
    /// 获取枚举成员的Description特性描述
    /// </summary>
    /// <param name="enums">枚举值</param>
    /// <returns>返回枚举成员的Description特性描述；如果未设置Description特性，则返回枚举成员的名称</returns>
    public static string GetDescription(this Enum enums)
    {
        var strValue = enums.ToString();
        var fieldinfo = enums.GetType().GetField(strValue);

        if (fieldinfo is null) return strValue;

        var objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (objs is null || objs.Length == 0) return strValue;

        var da = (DescriptionAttribute)objs[0];

        return da.Description;
    }

    /// <summary>
    /// 根据枚举值获取对应枚举成员的Description特性描述
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="value">枚举值（整数）</param>
    /// <returns>返回对应枚举成员的Description特性描述；如果未找到匹配的枚举成员，则返回默认值</returns>
    public static string GetDescriptionByValue<T>(int value)
    {
        foreach (var item in Enum.GetValues(typeof(T)))
        {
            Enum.TryParse(typeof(T), item.ToString(), out var obj);
            if (obj is not null)
                if (obj is Enum enumValue)
                    if (value == Convert.ToInt32(enumValue))
                        return enumValue.GetDescription();
        }

        return default!;
    }
}
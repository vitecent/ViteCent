#region

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

#endregion

namespace ViteCent.Core;

/// <summary>
/// JSON序列化和反序列化工具类，提供对象与JSON字符串之间的转换功能
/// </summary>
public static class BaseJson
{
    /// <summary>
    /// JSON序列化设置，配置了日期格式、大小写规则、空值处理等全局序列化选项
    /// </summary>
    private static readonly JsonSerializerSettings settings;

    /// <summary>
    /// 静态构造函数，初始化JSON序列化设置
    /// </summary>
    static BaseJson()
    {
        settings = new JsonSerializerSettings
        {
            DefaultValueHandling = DefaultValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.None,
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
            Converters =
            [
                new IsoDateTimeConverter
                {
                    DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                }
            ]
        };
    }

    /// <summary>
    /// 将JSON字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="json">要反序列化的JSON字符串</param>
    /// <returns>反序列化后的对象，如果输入为空或反序列化失败则返回类型默认值</returns>
    public static T DeJson<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default!;

        var result = JsonConvert.DeserializeObject<T>(json, settings);

        if (result is null)
            return default!;

        return result;
    }

    /// <summary>
    /// 将对象序列化为JSON字符串
    /// </summary>
    /// <param name="obj">要序列化的对象</param>
    /// <returns>序列化后的JSON字符串，如果输入对象为null则返回默认值</returns>
    public static string ToJson(this object obj)
    {
        if (obj is null)
            return default!;

        return JsonConvert.SerializeObject(obj, settings);
    }
}
#region

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseJson
{
    /// <summary>
    /// </summary>
    private static readonly JsonSerializerSettings settings;

    /// <summary>
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
            ],
        };
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T DeJson<T>(this string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return default!;

        var result = JsonConvert.DeserializeObject<T>(json, settings);

        if (result == null)
            return default!;

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        if (obj == null)
            return default!;

        return JsonConvert.SerializeObject(obj, settings);
    }
}
#region

using System.Text.Json;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseJson
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T DeJson<T>(this string json)
    {
        if (string.IsNullOrEmpty(json)) throw new Exception("json 不能为空");

        var result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Web);

        return result ?? default!;
    }

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ToJson(this object obj)
    {
        if (obj == null) throw new Exception("obj 不能为空");

        return JsonSerializer.Serialize(obj);
    }
}
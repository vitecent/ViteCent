#region

using System.Text.Json;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class JsonExtensions.
/// </summary>
public static class BaseJson
{
    /// <summary>
    ///     Des the json.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json">The json.</param>
    /// <returns>T.</returns>
    public static T DeJson<T>(this string json)
    {
        if (string.IsNullOrEmpty(json)) throw new Exception("json 不能为空");

        var result = JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Web);

        return result ?? default!;
    }

    /// <summary>
    ///     Converts to json.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>System.String.</returns>
    public static string ToJson(this object obj)
    {
        if (obj == null) throw new Exception("obj 不能为空");

        return JsonSerializer.Serialize(obj);
    }
}
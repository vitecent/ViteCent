#region

using System.Xml;
using System.Xml.Serialization;

#endregion

namespace ViteCent.Core;

/// <summary>
/// </summary>
public static class BaseXml
{
    /// <summary>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xml"></param>
    /// <returns></returns>
    public static T DeXml<T>(string xml)
    {
        if (string.IsNullOrEmpty(xml)) throw new Exception("xml 不能为空");

        using var reader = new StringReader(xml);

        if (reader == null) return default!;

        var result = new XmlSerializer(typeof(T)).Deserialize(reader);

        if (result == null) return default!;

        return (T)result;
    }

    /// <summary>
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static XmlDocument ToXml(this object obj)
    {
        if (obj == null) throw new Exception("obj 不能为空");

        using var writer = new StringWriter();
        new XmlSerializer(obj.GetType()).Serialize(writer, obj);

        var document = new XmlDocument();
        document.LoadXml(writer.ToString());

        return document;
    }
}
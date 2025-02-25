#region

using System.Xml;
using System.Xml.Serialization;

#endregion

namespace ViteCent.Core;

/// <summary>
///     Class XmlExtensions.
/// </summary>
public static class BaseXml
{
    /// <summary>
    ///     Des the XML.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="xml">The XML.</param>
    /// <returns>T.</returns>
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
    ///     Converts to xml.
    /// </summary>
    /// <param name="obj">The object.</param>
    /// <returns>XmlDocument.</returns>
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
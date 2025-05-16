#region

using System.Xml;
using System.Xml.Serialization;

#endregion

namespace ViteCent.Core;

/// <summary>
/// XML序列化和反序列化工具类，提供XML与对象之间的转换功能
/// </summary>
public static class BaseXml
{
    /// <summary>
    /// 将XML字符串反序列化为指定类型的对象
    /// </summary>
    /// <typeparam name="T">要反序列化的目标类型</typeparam>
    /// <param name="xml">XML格式的字符串</param>
    /// <returns>反序列化后的类型实例，如果反序列化失败则返回类型默认值</returns>
    /// <exception cref="Exception">当XML字符串为空时抛出异常</exception>
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
    /// 将对象序列化为XML文档
    /// </summary>
    /// <param name="obj">要序列化的对象实例</param>
    /// <returns>序列化后的XML文档对象</returns>
    /// <exception cref="Exception">当对象为null时抛出异常</exception>
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
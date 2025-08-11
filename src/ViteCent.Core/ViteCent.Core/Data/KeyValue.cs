namespace ViteCent.Core.Data;

/// <summary>
/// 键值对数据模型，用于存储键值对形式的数据
/// </summary>
/// <typeparam name="T">值的数据类型，必须是引用类型</typeparam>
public class KeyValue<T>
    where T : class
{
    /// <summary>
    /// 键名，用于标识键值对中的键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 值，键值对中存储的实际数据内容
    /// </summary>
    public T Value { get; set; } = default!;
}
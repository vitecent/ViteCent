namespace ViteCent.Core.Data;

/// <summary>
/// 带有泛型数据的API接口返回结果数据模型
/// </summary>
/// <typeparam name="T">返回的数据类型，必须是引用类型</typeparam>
public class DataResult<T> : BaseResult
    where T : class
{
    /// <summary>
    /// 初始化一个成功的带数据返回结果实例
    /// </summary>
    public DataResult()
    {
    }

    /// <summary>
    /// 初始化一个带有数据和消息内容的成功返回结果实例
    /// </summary>
    /// <param name="data">返回的数据内容</param>
    public DataResult(T data)
        : base(string.Empty)
    {
        Data = data;
    }

    /// <summary>
    /// 初始化一个错误的返回结果实例
    /// </summary>
    /// <param name="code">错误状态码</param>
    /// <param name="message">错误消息内容</param>
    public DataResult(int code, string message) : base(code, message)
    {
    }

    /// <summary>
    /// 返回的数据内容
    /// </summary>
    public T Data { get; set; } = default!;
}
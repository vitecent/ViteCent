namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public class DataResult<T> : BaseResult
    where T : class
{
    /// <summary>
    /// </summary>
    public DataResult()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="message"></param>
    public DataResult(T data, string message = "")
        : base(message)
    {
        Data = data;
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public DataResult(int code, string message) : base(code, message)
    {
    }

    /// <summary>
    /// </summary>
    public T Data { get; set; } = default!;
}
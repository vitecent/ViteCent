namespace ViteCent.Core.Data;

/// <summary>
/// API接口返回结果的基础数据模型
/// </summary>
public class BaseResult
{
    /// <summary>
    /// 初始化一个成功的返回结果实例
    /// </summary>
    public BaseResult()
    {
        Success = true;
    }

    /// <summary>
    /// 初始化一个带有消息内容的成功返回结果实例
    /// </summary>
    /// <param name="content">返回的消息内容</param>
    public BaseResult(string content)
    {
        Success = true;
        Message = content;
    }

    /// <summary>
    /// 初始化一个错误的返回结果实例
    /// </summary>
    /// <param name="code">错误状态码</param>
    /// <param name="message">错误消息内容</param>
    public BaseResult(int code, string message)
    {
        Success = false;
        Code = code;
        Message = message;
    }

    /// <summary>
    /// 状态码，默认200表示成功
    /// </summary>
    public int Code { get; set; } = 200;

    /// <summary>
    /// 返回的消息内容
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 操作是否成功
    /// </summary>
    public bool Success { get; set; }
}
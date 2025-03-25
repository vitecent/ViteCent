namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class BaseResult
{
    /// <summary>
    /// </summary>
    public BaseResult()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="content"></param>
    public BaseResult(string content)
    {
        Message = content;
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public BaseResult(int code, string message)
    {
        Success = false;
        Code = code;
        Message = message;
    }

    /// <summary>
    /// </summary>
    public int Code { get; set; } = 200;

    /// <summary>
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public bool Success { get; set; } = true;
}
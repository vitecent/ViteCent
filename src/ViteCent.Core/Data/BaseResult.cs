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
        Content = content;
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="content"></param>
    public BaseResult(int code, string content)
    {
        IsSuccessStatusCode = false;
        StatusCode = code;
        Content = content;
    }

    /// <summary>
    /// </summary>
    /// <value>The content.</value>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value><c>true</c> if succeed; otherwise, <c>false</c>.</value>
    public bool IsSuccessStatusCode { get; set; } = true;

    /// <summary>
    /// </summary>
    /// <value>The code.</value>
    public int StatusCode { get; set; } = 200;
}
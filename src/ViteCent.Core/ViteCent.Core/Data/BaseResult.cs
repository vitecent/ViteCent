namespace ViteCent.Core.Data;

/// <summary>
///     Class BaseResult.
/// </summary>
public class BaseResult
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseResult" /> class.
    /// </summary>
    public BaseResult()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseResult" /> class.
    /// </summary>
    /// <param name="content">The content.</param>
    public BaseResult(string content)
    {
        Content = content;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseResult" /> class.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="content">The content.</param>
    public BaseResult(int code, string content)
    {
        IsSuccessStatusCode = false;
        StatusCode = code;
        Content = content;
    }

    /// <summary>
    ///     Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    ///     Gets or sets a value indicating whether this <see cref="BaseResult" /> is succeed.
    /// </summary>
    /// <value><c>true</c> if succeed; otherwise, <c>false</c>.</value>
    public bool IsSuccessStatusCode { get; set; } = true;

    /// <summary>
    ///     Gets or sets the code.
    /// </summary>
    /// <value>The code.</value>
    public int StatusCode { get; set; } = 200;
}
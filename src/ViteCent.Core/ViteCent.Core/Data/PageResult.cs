namespace ViteCent.Core.Data;

/// <summary>
///     Class PageResult. Implements the <see cref="ViteCent.Core.Data.BaseResult" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="ViteCent.Core.Data.BaseResult" />
public class PageResult<T> : BaseResult
    where T : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResult{T}" /> class.
    /// </summary>
    public PageResult()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResult{T}" /> class.
    /// </summary>
    /// <param name="offset">The offset.</param>
    /// <param name="limit">The limit.</param>
    /// <param name="total">The total.</param>
    /// <param name="rows">The rows.</param>
    public PageResult(int offset, int limit, int total, List<T> rows)
    {
        Offset = offset;
        Limit = limit;
        Total = total;
        Rows = rows;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResult{T}" /> class.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="message">The message.</param>
    public PageResult(int code, string message) : base(code, message)
    {
        Offset = 1;
        Limit = 10;
        Total = 0;
        Rows = default!;
    }

    /// <summary>
    ///     Gets or sets the limit.
    /// </summary>
    /// <value>The limit.</value>
    public int Limit { get; set; } = 10;

    /// <summary>
    ///     Gets or sets the offset.
    /// </summary>
    /// <value>The offset.</value>
    public int Offset { get; set; } = 1;

    /// <summary>
    ///     Gets or sets the rows.
    /// </summary>
    /// <value>The rows.</value>
    public List<T> Rows { get; set; } = [];

    /// <summary>
    ///     Gets or sets the total.
    /// </summary>
    /// <value>The total.</value>
    public int Total { get; set; }
}
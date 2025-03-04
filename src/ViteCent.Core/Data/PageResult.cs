namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageResult<T> : BaseResult
    where T : class
{
    /// <summary>
    /// </summary>
    public PageResult()
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="offset"></param>
    /// <param name="limit"></param>
    /// <param name="total"></param>
    /// <param name="rows"></param>
    public PageResult(int offset, int limit, int total, List<T> rows)
    {
        Offset = offset;
        Limit = limit;
        Total = total;
        Rows = rows;
    }

    /// <summary>
    /// </summary>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public PageResult(int code, string message) : base(code, message)
    {
        Offset = 1;
        Limit = 10;
        Total = 0;
    }

    /// <summary>
    /// </summary>
    /// <value>The limit.</value>
    public int Limit { get; set; } = 10;

    /// <summary>
    /// </summary>
    /// <value>The offset.</value>
    public int Offset { get; set; } = 1;

    /// <summary>
    /// </summary>
    /// <value>The rows.</value>
    public List<T> Rows { get; set; } = [];

    /// <summary>
    /// </summary>
    /// <value>The total.</value>
    public int Total { get; set; }
}
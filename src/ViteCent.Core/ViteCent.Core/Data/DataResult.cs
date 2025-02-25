namespace ViteCent.Core.Data;

/// <summary>
///     Class DataResult. Implements the <see cref="ViteCent.Core.Data.BaseResult" />
/// </summary>
/// <typeparam name="T"></typeparam>
/// <seealso cref="ViteCent.Core.Data.BaseResult" />
public class DataResult<T> : BaseResult
    where T : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DataResult{T}" /> class.
    /// </summary>
    public DataResult()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataResult{T}" /> class.
    /// </summary>
    /// <param name="entity">The Entity.</param>
    public DataResult(T entity)
    {
        Data = entity;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DataResult{T}" /> class.
    /// </summary>
    /// <param name="code">The code.</param>
    /// <param name="message">The message.</param>
    public DataResult(int code, string message) : base(code, message)
    {
        Data = default!;
    }

    /// <summary>
    ///     Gets or sets the data.
    /// </summary>
    /// <value>The data.</value>
    public T Data { get; set; } = default!;
}
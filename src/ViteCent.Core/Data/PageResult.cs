namespace ViteCent.Core.Data;

/// <summary>
/// 分页数据返回结果模型
/// </summary>
/// <typeparam name="T">数据实体类型</typeparam>
public class PageResult<T> : BaseResult
    where T : class
{
    /// <summary>
    /// 初始化分页数据返回结果模型
    /// </summary>
    public PageResult()
    {
    }

    /// <summary>
    /// 初始化分页数据返回结果模型
    /// </summary>
    /// <param name="offset">当前页码</param>
    /// <param name="limit">每页记录数</param>
    /// <param name="total">总记录数</param>
    /// <param name="rows">数据集合</param>
    /// <param name="message">返回消息</param>
    public PageResult(int offset, int limit, int total, List<T> rows, string message = "")
        : base(message)
    {
        Offset = offset;
        Limit = limit;
        Total = total;
        Rows = rows;
    }

    /// <summary>
    /// 初始化分页数据返回结果模型
    /// </summary>
    /// <param name="code">返回代码</param>
    /// <param name="message">返回消息</param>
    public PageResult(int code, string message) : base(code, message)
    {
        Offset = 1;
        Limit = 10;
        Total = 0;
    }

    /// <summary>
    /// 每页记录数
    /// </summary>
    public int Limit { get; set; } = 10;

    /// <summary>
    /// 当前页码
    /// </summary>
    public int Offset { get; set; } = 1;

    /// <summary>
    /// 数据集合
    /// </summary>
    public List<T> Rows { get; set; } = [];

    /// <summary>
    /// 总记录数
    /// </summary>
    public int Total { get; set; }
}
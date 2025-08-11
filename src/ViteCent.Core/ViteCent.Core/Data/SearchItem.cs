#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// 搜索条件项数据模型，用于定义单个查询条件的具体信息
/// </summary>
public class SearchItem
{
    /// <summary>
    /// 查询字段名称，表示要搜索的数据字段
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 条件分组，用于将多个搜索条件归类到同一组中
    /// </summary>
    public string Group { get; set; } = string.Empty;

    /// <summary>
    /// 查询方式，定义字段值的比较方法，默认为等于(Equal)
    /// </summary>
    public SearchEnum Method { get; set; } = SearchEnum.Equal;

    /// <summary>
    /// 查询值，指定要搜索的具体数据值
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
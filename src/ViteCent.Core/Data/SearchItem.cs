#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class SearchItem
{
    /// <summary>
    /// </summary>
    /// <value>The field.</value>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The group.</value>
    public string Group { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The method.</value>
    public SearchEnum Method { get; set; } = SearchEnum.Equal;

    /// <summary>
    /// </summary>
    /// <value>The value.</value>
    public object Value { get; set; } = string.Empty;
}
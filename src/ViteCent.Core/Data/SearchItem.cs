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
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public string Group { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public SearchEnum Method { get; set; } = SearchEnum.Equal;

    /// <summary>
    /// </summary>
    public string Value { get; set; } = string.Empty;
}
#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// </summary>
public class OrderField
{
    /// <summary>
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    public OrderEnum OrderType { get; set; } = OrderEnum.Desc;
}
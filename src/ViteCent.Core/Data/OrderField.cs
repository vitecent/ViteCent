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
    /// <value>The field.</value>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// </summary>
    /// <value>The type of the order.</value>
    public OrderEnum OrderType { get; set; } = OrderEnum.Desc;
}
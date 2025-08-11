#region

using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Core.Data;

/// <summary>
/// 排序字段模型，用于定义数据排序的字段信息
/// </summary>
public class OrderField
{
    /// <summary>
    /// 排序字段名称
    /// </summary>
    public string Field { get; set; } = string.Empty;

    /// <summary>
    /// 排序类型，默认为降序(Desc)
    /// </summary>
    public OrderEnum OrderType { get; set; } = OrderEnum.Desc;
}
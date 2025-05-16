namespace ViteCent.Core.Enums;

/// <summary>
/// 数据关联类型枚举，用于定义数据之间的关联关系
/// </summary>
public enum LinkEnum
{
    /// <summary>
    /// 包含关系，表示数据之间存在包含或属于的关联
    /// </summary>
    InSet = 1,

    /// <summary>
    /// 不包含关系，表示数据之间不存在包含或属于的关联
    /// </summary>
    OutSet = 2
}
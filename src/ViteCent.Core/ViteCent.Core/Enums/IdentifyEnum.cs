namespace ViteCent.Core.Enums;

/// <summary>
/// 标识生成时间单位枚举，用于定义生成标识时的时间粒度
/// </summary>
public enum IdentifyEnum
{
    /// <summary>
    /// 按天生成标识，以天为单位生成唯一标识
    /// </summary>
    Day = 1,

    /// <summary>
    /// 按月生成标识，以月为单位生成唯一标识
    /// </summary>
    Month = 2,

    /// <summary>
    /// 按年生成标识，以年为单位生成唯一标识
    /// </summary>
    Year = 4
}
namespace ViteCent.Core.Enums;

/// <summary>
/// 主题操作类型枚举，用于定义对主题内容的各种操作方式
/// </summary>
public enum TopicEnum
{
    /// <summary>
    /// 添加操作，用于创建新的主题内容
    /// </summary>
    Add = 1,

    /// <summary>
    /// 编辑操作，用于修改现有的主题内容
    /// </summary>
    Edit = 2,

    /// <summary>
    /// 删除操作，用于移除指定的主题内容
    /// </summary>
    Delete = 3,

    /// <summary>
    /// 启用操作，用于激活或显示主题内容
    /// </summary>
    Enable = 4,

    /// <summary>
    /// 禁用操作，用于停用或隐藏主题内容
    /// </summary>
    Disable = 5
}
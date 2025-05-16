namespace ViteCent.Core.Enums;

/// <summary>
/// 基本操作类型枚举，用于定义系统中的基础操作行为
/// </summary>
public enum OperationEnum
{
    /// <summary>
    /// 添加操作，用于创建新的数据记录或资源
    /// </summary>
    Add = 1,

    /// <summary>
    /// 编辑操作，用于修改现有的数据记录或资源
    /// </summary>
    Edit = 2,

    /// <summary>
    /// 启用操作，用于激活或允许使用指定的功能或资源
    /// </summary>
    Enable = 3,

    /// <summary>
    /// 禁用操作，用于停用或限制使用指定的功能或资源
    /// </summary>
    Disable = 4,

    /// <summary>
    /// 删除操作，用于移除指定的数据记录或资源
    /// </summary>
    Delete = 5
}
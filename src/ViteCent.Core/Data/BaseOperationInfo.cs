namespace ViteCent.Core.Data;

/// <summary>
/// 操作基础信息数据模型
/// </summary>
public class BaseOperationInfo
{
    /// <summary>
    /// 操作编码，用于标识操作的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 操作唯一标识，系统内部使用的唯一ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 操作名称，操作的完整描述名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 操作序号，用于定义操作的排序顺序
    /// </summary>
    public int Sequence { get; set; }
}
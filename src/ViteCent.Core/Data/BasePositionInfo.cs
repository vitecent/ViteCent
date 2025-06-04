namespace ViteCent.Core.Data;

/// <summary>
/// 职位基础信息数据模型
/// </summary>
public class BasePositionInfo
{
    /// <summary>
    /// 职位编码，用于标识职位的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 职位唯一标识，系统内部使用的唯一标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称，职位的完整名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
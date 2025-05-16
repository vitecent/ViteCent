namespace ViteCent.Core.Data;

/// <summary>
/// 系统基础信息数据模型
/// </summary>
public class BaseSystemInfo
{
    /// <summary>
    /// 系统编码，用于标识系统的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 系统唯一标识，系统内部使用的唯一ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 系统名称，系统的完整名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 系统资源列表，包含该系统下的所有资源信息
    /// </summary>
    public List<BaseResourceInfo> Resources { get; set; } = [];

    /// <summary>
    /// 系统序号，用于定义系统的排序顺序
    /// </summary>
    public int Sequence { get; set; }
}
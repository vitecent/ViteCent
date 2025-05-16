namespace ViteCent.Core.Data;

/// <summary>
/// 资源基础信息数据模型
/// </summary>
public class BaseResourceInfo
{
    /// <summary>
    /// 资源编码，用于标识资源的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 资源唯一标识，系统内部使用的唯一ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 资源名称，资源的完整名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 资源操作列表，包含该资源支持的所有操作信息
    /// </summary>
    public List<BaseOperationInfo> Operations { get; set; } = [];

    /// <summary>
    /// 资源序号，用于定义资源的排序顺序
    /// </summary>
    public int Sequence { get; set; }
}
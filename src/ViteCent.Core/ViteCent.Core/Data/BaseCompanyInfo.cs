namespace ViteCent.Core.Data;

/// <summary>
/// 公司基础信息数据模型
/// </summary>
public class BaseCompanyInfo
{
    /// <summary>
    /// 公司编码，用于标识公司的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 公司唯一标识，系统内部使用的唯一标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 公司名称，公司的完整注册名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
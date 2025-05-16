namespace ViteCent.Core.Data;

/// <summary>
/// 部门基础信息数据模型
/// </summary>
public class BaseDepartmentInfo
{
    /// <summary>
    /// 部门编码，用于标识部门的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 部门唯一标识，系统内部使用的唯一ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称，部门的完整名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}
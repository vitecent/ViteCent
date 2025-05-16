namespace ViteCent.Core.Data;

/// <summary>
/// 用户基础信息数据模型
/// </summary>
public class BaseUserInfo
{
    /// <summary>
    /// 用户权限列表，包含用户所拥有的权限标识
    /// </summary>
    public List<string> Auth { get; set; } = [];

    /// <summary>
    /// 用户权限详细信息列表，包含权限的完整信息
    /// </summary>
    public List<BaseSystemInfo> AuthInfo { get; set; } = [];

    /// <summary>
    /// 用户编码，用于标识用户的唯一业务代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 用户所属公司信息
    /// </summary>
    public BaseCompanyInfo Company { get; set; } = new();

    /// <summary>
    /// 用户所属部门信息
    /// </summary>
    public BaseDepartmentInfo Department { get; set; } = new();

    /// <summary>
    /// 用户唯一标识，系统内部使用的唯一ID
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 是否为超级管理员，0表示普通用户，1表示超级管理员
    /// </summary>
    public int IsSuper { get; set; }

    /// <summary>
    /// 用户名称，用户的显示名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 用户职位信息
    /// </summary>
    public BasePositionInfo Position { get; set; } = new();

    /// <summary>
    /// 用户访问令牌，用于身份验证和授权
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
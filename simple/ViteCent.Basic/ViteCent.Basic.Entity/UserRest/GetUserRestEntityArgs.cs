#region

using MediatR;

#endregion

namespace ViteCent.Basic.Entity.UserRest;

/// <summary>
/// 获取调休申请数据参数
/// </summary>
[Serializable]
public class GetUserRestEntityArgs : IRequest<UserRestEntity>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 部门标识
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// 用户标识
    /// </summary>
    public string UserId { get; set; } = string.Empty;
}
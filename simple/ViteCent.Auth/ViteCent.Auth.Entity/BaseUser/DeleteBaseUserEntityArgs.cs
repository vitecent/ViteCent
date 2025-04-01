#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseUser;

/// <summary>
/// 删除用户信息数据参数
/// </summary>
[Serializable]
public class DeleteBaseUserEntityArgs : IRequest<BaseResult>
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
    /// 职位标识
    /// </summary>
    public string PositionId { get; set; } = string.Empty;
}
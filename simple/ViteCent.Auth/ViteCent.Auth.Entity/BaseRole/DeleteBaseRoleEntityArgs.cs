#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseRole;

/// <summary>
/// 删除角色信息数据参数
/// </summary>
[Serializable]
public class DeleteBaseRoleEntityArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 公司标识
    /// </summary>
    public string CompanyId { get; set; } = string.Empty;

    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
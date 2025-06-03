/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

#region 引入命名空间

using MediatR;
using ViteCent.Core.Data;

#endregion 引入命名空间

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// 批量新增角色权限模型
/// </summary>
[Serializable]
public class AddBaseRolePermissionEntityListArgs : IRequest<BaseResult>
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public List<AddBaseRolePermissionEntity> Items = [];
}
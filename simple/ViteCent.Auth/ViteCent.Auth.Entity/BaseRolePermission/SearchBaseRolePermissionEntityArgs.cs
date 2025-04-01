#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Entity.BaseRolePermission;

/// <summary>
/// 搜索角色权限数据参数
/// </summary>
[Serializable]
public class SearchBaseRolePermissionEntityArgs : SearchArgs, IRequest<List<BaseRolePermissionEntity>>
{
}
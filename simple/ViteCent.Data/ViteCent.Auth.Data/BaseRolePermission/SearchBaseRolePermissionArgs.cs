#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// 搜索角色权限参数
/// </summary>
[Serializable]
public class SearchBaseRolePermissionArgs : SearchArgs, IRequest<PageResult<BaseRolePermissionResult>>
{
}
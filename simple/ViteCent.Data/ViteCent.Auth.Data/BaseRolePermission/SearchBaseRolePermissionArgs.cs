#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
[Serializable]
public class SearchBaseRolePermissionArgs : SearchArgs, IRequest<PageResult<BaseRolePermissionResult>>
{
}
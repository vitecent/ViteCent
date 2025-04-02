/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * **********************************
 */

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
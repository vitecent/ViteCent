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
/// 批量新增角色权限参数
/// </summary>
[Serializable]
public class AddBaseRolePermissionListArgs : BaseArgs, IRequest<BaseResult>
{
	/// <summary>
	/// 数据
    /// </summary>
	public List<AddBaseRolePermissionArgs> Items = [];
}
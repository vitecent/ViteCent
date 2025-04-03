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

namespace ViteCent.Auth.Entity.BaseUserRole;

/// <summary>
/// 批量新增用户角色数据参数
/// </summary>
[Serializable]
public class AddBaseUserRoleEntityListArgs : IRequest<BaseResult>
{
	/// <summary>
	/// 数据
    /// </summary>
	public List<AddBaseUserRoleEntity> Items = [];
}
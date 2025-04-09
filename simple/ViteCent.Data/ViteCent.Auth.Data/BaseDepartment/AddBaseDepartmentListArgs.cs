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

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// 批量新增部门信息参数
/// </summary>
[Serializable]
public class AddBaseDepartmentListArgs : BaseArgs, IRequest<BaseResult>
{
	/// <summary>
	/// 部门信息
	/// </summary>
	public List<AddBaseDepartmentArgs> Items = [];
}
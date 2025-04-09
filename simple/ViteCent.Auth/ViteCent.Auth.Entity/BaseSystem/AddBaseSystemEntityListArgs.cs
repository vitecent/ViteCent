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

namespace ViteCent.Auth.Entity.BaseSystem;

/// <summary>
/// 批量新增系统信息参数
/// </summary>
[Serializable]
public class AddBaseSystemEntityListArgs : IRequest<BaseResult>
{
	/// <summary>
	/// 系统信息
	/// </summary>
	public List<AddBaseSystemEntity> Items = [];
}
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

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// 批量新增请假申请参数
/// </summary>
[Serializable]
public class AddUserLeaveListArgs : BaseArgs, IRequest<BaseResult>
{
	/// <summary>
	/// 数据
	/// </summary>
	public List<AddUserLeaveArgs> Items = [];
}
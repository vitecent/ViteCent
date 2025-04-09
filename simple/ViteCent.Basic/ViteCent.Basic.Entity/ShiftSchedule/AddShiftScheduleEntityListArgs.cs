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

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 批量新增换班申请参数
/// </summary>
[Serializable]
public class AddShiftScheduleEntityListArgs : IRequest<BaseResult>
{
	/// <summary>
	/// 换班申请
	/// </summary>
	public List<AddShiftScheduleEntity> Items = [];
}
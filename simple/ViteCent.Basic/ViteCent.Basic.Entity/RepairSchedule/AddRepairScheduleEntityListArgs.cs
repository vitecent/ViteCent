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

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// 批量新增补卡申请参数
/// </summary>
[Serializable]
public class AddRepairScheduleEntityListArgs : IRequest<BaseResult>
{
	// <summary>
	/// 补卡申请
	/// </summary>
	public List<AddRepairScheduleEntity> Items = [];
}
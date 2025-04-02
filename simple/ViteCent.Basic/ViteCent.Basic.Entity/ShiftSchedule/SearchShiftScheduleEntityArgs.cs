/*
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 搜索换班申请数据参数
/// </summary>
[Serializable]
public class SearchShiftScheduleEntityArgs : SearchArgs, IRequest<List<ShiftScheduleEntity>>
{
}
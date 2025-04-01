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
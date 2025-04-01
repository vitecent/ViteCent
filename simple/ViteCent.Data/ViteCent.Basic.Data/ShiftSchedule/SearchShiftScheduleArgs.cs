#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// 搜索换班申请参数
/// </summary>
[Serializable]
public class SearchShiftScheduleArgs : SearchArgs, IRequest<PageResult<ShiftScheduleResult>>
{
}
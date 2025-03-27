#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchShiftScheduleArgs : SearchArgs, IRequest<PageResult<ShiftScheduleResult>>
{
}
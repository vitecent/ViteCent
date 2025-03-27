#region

using MediatR;
using ViteCent.Basic.Entity.ShiftSchedule;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class SearchShiftScheduleEntityArgs : SearchArgs, IRequest<List<ShiftScheduleEntity>>
{
}
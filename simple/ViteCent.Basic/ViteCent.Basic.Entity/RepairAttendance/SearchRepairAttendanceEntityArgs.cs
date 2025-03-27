#region

using MediatR;
using ViteCent.Basic.Entity.RepairAttendance;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public class SearchRepairAttendanceEntityArgs : SearchArgs, IRequest<List<RepairAttendanceEntity>>
{
}
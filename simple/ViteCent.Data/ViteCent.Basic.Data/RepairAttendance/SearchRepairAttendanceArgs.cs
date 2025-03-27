#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public class SearchRepairAttendanceArgs : SearchArgs, IRequest<PageResult<RepairAttendanceResult>>
{
}
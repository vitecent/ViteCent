#region

using MediatR;
using ViteCent.Basic.Entity.Attendance;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Entity.Attendance;

/// <summary>
/// </summary>
[Serializable]
public class SearchAttendanceEntityArgs : SearchArgs, IRequest<List<AttendanceEntity>>
{
}
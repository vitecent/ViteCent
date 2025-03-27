#region

using MediatR;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.Attendance;

/// <summary>
/// </summary>
[Serializable]
public class SearchAttendanceArgs : SearchArgs, IRequest<PageResult<AttendanceResult>>
{
}
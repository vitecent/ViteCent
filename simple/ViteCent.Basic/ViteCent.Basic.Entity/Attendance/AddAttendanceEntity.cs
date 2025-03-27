#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.Attendance;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("attendance")]
public class AddAttendanceEntity : AttendanceEntity
{

}
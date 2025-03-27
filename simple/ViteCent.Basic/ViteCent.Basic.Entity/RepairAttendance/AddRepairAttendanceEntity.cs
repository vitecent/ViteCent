#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("repair_attendance")]
public class AddRepairAttendanceEntity : RepairAttendanceEntity
{

}
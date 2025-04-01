#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("repair_schedule")]
public class AddRepairScheduleEntity : RepairScheduleEntity
{
}
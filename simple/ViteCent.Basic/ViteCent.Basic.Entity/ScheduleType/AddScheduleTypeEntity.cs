#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("schedule_type")]
public class AddScheduleTypeEntity : ScheduleTypeEntity
{
}
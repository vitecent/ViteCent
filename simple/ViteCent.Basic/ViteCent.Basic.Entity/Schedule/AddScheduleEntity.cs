#region

using MediatR;
using SqlSugar;
using ViteCent.Core.Data;
using ViteCent.Core.Orm.SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// </summary>
[Serializable]
[SugarTable("schedule")]
public class AddScheduleEntity : ScheduleEntity
{

}
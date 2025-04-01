#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.Schedule;

/// <summary>
/// 新增排班信息数据参数
/// </summary>
[Serializable]
[SugarTable("schedule")]
public class AddScheduleEntity : ScheduleEntity
{
}
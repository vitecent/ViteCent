#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.ScheduleType;

/// <summary>
/// 新增基础排班数据参数
/// </summary>
[Serializable]
[SugarTable("schedule_type")]
public class AddScheduleTypeEntity : ScheduleTypeEntity
{
}
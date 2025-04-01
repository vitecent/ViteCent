#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.ShiftSchedule;

/// <summary>
/// 新增换班申请数据参数
/// </summary>
[Serializable]
[SugarTable("shift_schedule")]
public class AddShiftScheduleEntity : ShiftScheduleEntity
{
}
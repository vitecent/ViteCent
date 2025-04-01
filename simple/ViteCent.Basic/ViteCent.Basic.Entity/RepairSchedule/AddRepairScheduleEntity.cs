#region

using SqlSugar;

#endregion

namespace ViteCent.Basic.Entity.RepairSchedule;

/// <summary>
/// 新增补卡申请数据参数
/// </summary>
[Serializable]
[SugarTable("repair_schedule")]
public class AddRepairScheduleEntity : RepairScheduleEntity
{
}
namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// 编辑补卡申请参数
/// </summary>
[Serializable]
public class EditRepairScheduleArgs : AddRepairScheduleArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// 编辑换班申请参数
/// </summary>
[Serializable]
public class EditShiftScheduleArgs : AddShiftScheduleArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
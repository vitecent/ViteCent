namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// 编辑排班信息参数
/// </summary>
[Serializable]
public class EditScheduleArgs : AddScheduleArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
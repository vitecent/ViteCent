namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// 编辑基础排班参数
/// </summary>
[Serializable]
public class EditScheduleTypeArgs : AddScheduleTypeArgs
{
    /// <summary>
    /// 标识
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
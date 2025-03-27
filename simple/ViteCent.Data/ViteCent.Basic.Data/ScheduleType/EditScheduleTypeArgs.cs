namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public class EditScheduleTypeArgs : AddScheduleTypeArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
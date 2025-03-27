namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
[Serializable]
public class EditScheduleArgs : AddScheduleArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
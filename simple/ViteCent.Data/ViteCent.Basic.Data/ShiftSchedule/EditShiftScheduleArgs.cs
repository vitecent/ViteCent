namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public class EditShiftScheduleArgs : AddShiftScheduleArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
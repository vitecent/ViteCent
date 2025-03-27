namespace ViteCent.Basic.Data.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public class EditRepairAttendanceArgs : AddRepairAttendanceArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
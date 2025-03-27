namespace ViteCent.Basic.Data.Attendance;

/// <summary>
/// </summary>
[Serializable]
public class EditAttendanceArgs : AddAttendanceArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
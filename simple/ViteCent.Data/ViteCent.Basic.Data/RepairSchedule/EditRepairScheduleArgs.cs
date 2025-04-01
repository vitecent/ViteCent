namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
[Serializable]
public class EditRepairScheduleArgs : AddRepairScheduleArgs
{
    /// <summary>
    /// </summary>
    public string Id { get; set; } = string.Empty;
}
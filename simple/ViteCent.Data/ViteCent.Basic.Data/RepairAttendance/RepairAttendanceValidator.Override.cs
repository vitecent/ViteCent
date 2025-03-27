#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.RepairAttendance;

/// <summary>
/// </summary>
public partial class RepairAttendanceValidator : AbstractValidator<AddRepairAttendanceArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
public partial class RepairScheduleValidator : AbstractValidator<AddRepairScheduleArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
    }
}
#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public partial class ScheduleValidator : AbstractValidator<AddScheduleArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
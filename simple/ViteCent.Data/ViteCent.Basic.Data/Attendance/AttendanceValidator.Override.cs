#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Attendance;

/// <summary>
/// </summary>
public partial class AttendanceValidator : AbstractValidator<AddAttendanceArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
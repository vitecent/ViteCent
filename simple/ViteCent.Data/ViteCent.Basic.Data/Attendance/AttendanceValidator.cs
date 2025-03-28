#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Attendance;

/// <summary>
/// </summary>
[Serializable]
public partial class AttendanceValidator : AbstractValidator<AddAttendanceArgs>
{
    /// <summary>
    /// </summary>
    public AttendanceValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator();
    }
}
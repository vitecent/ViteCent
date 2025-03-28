#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
[Serializable]
public partial class ShiftScheduleValidator : AbstractValidator<AddShiftScheduleArgs>
{
    /// <summary>
    /// </summary>
    public ShiftScheduleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");
        RuleFor(x => x.ShiftDepartmentId).NotNull().NotEmpty().WithMessage("换班部门标识不能为空");
        RuleFor(x => x.ShiftScheduleId).NotNull().NotEmpty().WithMessage("换班排班标识不能为空");
        RuleFor(x => x.ShiftUserId).NotNull().NotEmpty().WithMessage("换班用户标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator();
    }
}
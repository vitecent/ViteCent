#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.RepairAttendance;

/// <summary>
/// </summary>
[Serializable]
public partial class RepairAttendanceValidator : AbstractValidator<AddRepairAttendanceArgs>
{
    /// <summary>
    /// </summary>
    public RepairAttendanceValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.RepairTime).NotNull().NotEmpty().WithMessage("补卡时间不能为空");
        RuleFor(x => x.RepairType).NotNull().NotEmpty().WithMessage("补卡类型不能为空");
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");
        OverrideValidator();
    }
}
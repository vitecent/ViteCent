#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
[Serializable]
public partial class ScheduleTypeValidator : AbstractValidator<AddScheduleTypeArgs>
{
    /// <summary>
    /// </summary>
    public ScheduleTypeValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.EndTime).NotNull().NotEmpty().WithMessage("结束时间不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.Overnight).NotNull().NotEmpty().WithMessage("是否跨天不能为空");
        RuleFor(x => x.StartTime).NotNull().NotEmpty().WithMessage("开始时间不能为空");

        OverrideValidator();
    }
}
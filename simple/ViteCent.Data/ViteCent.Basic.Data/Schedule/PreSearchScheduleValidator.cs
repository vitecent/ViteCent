#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public class PreSearchScheduleValidator : AbstractValidator<PreSearchScheduleArgs>
{
    /// <summary>
    /// </summary>
    public PreSearchScheduleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.StartTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("开始时间不能为空");
        RuleFor(x => x.EndTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("结束时间不能为空");

        RuleFor(x => x).Must(x => x.EndTime > x.StartTime).WithMessage("结束时间必须大于开始时间");
    }
}
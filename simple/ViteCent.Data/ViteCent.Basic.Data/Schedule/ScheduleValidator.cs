#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// 排班信息验证器
/// </summary>
[Serializable]
public partial class ScheduleValidator : AbstractValidator<AddScheduleArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public ScheduleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.EndTime).NotNull().NotEmpty().WithMessage("结束时间不能为空");
        RuleFor(x => x.FirstTime).NotNull().NotEmpty().WithMessage("上班时间不能为空");
        RuleFor(x => x.LastTime).NotNull().NotEmpty().WithMessage("下班时间不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.StartTime).NotNull().NotEmpty().WithMessage("开始时间不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator();
    }
}
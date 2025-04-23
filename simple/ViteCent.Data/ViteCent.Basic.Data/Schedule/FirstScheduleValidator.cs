#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public class FirstScheduleValidator : AbstractValidator<FirstScheduleArgs>
{
    /// <summary>
    /// </summary>
    public FirstScheduleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("排班标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        var models = new List<int> { (int)ModelEnum.Web, (int)ModelEnum.Finger };

        RuleFor(x => x.Model).Must(x => models.Contains(x)).WithMessage("打卡类型不存在");
    }
}
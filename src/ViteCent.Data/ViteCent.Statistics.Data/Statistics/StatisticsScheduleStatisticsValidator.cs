#region

using FluentValidation;

#endregion

namespace ViteCent.Statistics.Data.Statistics;

/// <summary>
/// </summary>
public class StatisticsScheduleStatisticsValidator : AbstractValidator<StatisticsScheduleStatisticsArgs>
{
    /// <summary>
    /// </summary>
    public StatisticsScheduleStatisticsValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.Type).Must(x => x != default).WithMessage("类型不能为空");

        RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("日期不能为空");
    }
}
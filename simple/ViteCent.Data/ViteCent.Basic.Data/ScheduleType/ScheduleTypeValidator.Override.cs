/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
public partial class ScheduleTypeValidator : AbstractValidator<AddScheduleTypeArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate)
    {
        RuleFor(x => x.StartTime).Matches(BaseConst.Time).WithMessage("开始时间格式错误");

        RuleFor(x => x.EndTime).Matches(BaseConst.Time).WithMessage("结束时间格式错误");

        var scheduleTypes = new List<int> { (int)ScheduleTypeEnum.Schedule, (int)ScheduleTypeEnum.Leave, (int)ScheduleTypeEnum.Rest };

        RuleFor(x => x.ScheduleType).Must(x => scheduleTypes.Contains(x)).WithMessage("类型不存在");
    }
}
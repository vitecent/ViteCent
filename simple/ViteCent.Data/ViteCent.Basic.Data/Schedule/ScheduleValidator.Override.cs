/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.Schedule;

/// <summary>
/// </summary>
public partial class ScheduleValidator : AbstractValidator<AddScheduleArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate)
    {
        var status = new List<int>
            { (int)ScheduleEnum.Normal, (int)ScheduleEnum.Late, (int)ScheduleEnum.Early, (int)ScheduleEnum.None };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// </summary>
public partial class ShiftScheduleValidator : AbstractValidator<AddShiftScheduleArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate)
    {
        var status = new List<int>
            { (int)ShiftScheduleEnum.Apply, (int)ShiftScheduleEnum.Pass, (int)ShiftScheduleEnum.NoPass };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");

        RuleFor(x => x).Must(x => x.ShiftUserId != x.UserId).WithMessage("换班人不能相同");
    }
}
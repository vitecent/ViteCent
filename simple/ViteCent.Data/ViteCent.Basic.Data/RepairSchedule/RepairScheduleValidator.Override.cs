/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
public partial class RepairScheduleValidator : AbstractValidator<AddRepairScheduleArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate)
    {
        var status = new List<int>
            { (int)RepairScheduleEnum.Apply, (int)RepairScheduleEnum.Pass, (int)RepairScheduleEnum.NoPass };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
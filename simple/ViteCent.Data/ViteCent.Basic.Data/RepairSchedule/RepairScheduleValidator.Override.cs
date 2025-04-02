/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using FluentValidation;
using ViteCent.Basic.Data.UserLeave;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// </summary>
public partial class RepairScheduleValidator : AbstractValidator<AddRepairScheduleArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
        var status = new List<int>() { (int)RepairScheduleEnum.Apply, (int)RepairScheduleEnum.Pass, (int)RepairScheduleEnum.NoPass };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
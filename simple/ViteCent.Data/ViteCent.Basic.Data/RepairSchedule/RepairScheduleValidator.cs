/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// 补卡申请验证器
/// </summary>
[Serializable]
public partial class RepairScheduleValidator : AbstractValidator<AddRepairScheduleArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="validate"></param>
    public RepairScheduleValidator(bool validate = false)
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.RepairTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("补卡时间不能为空");
        RuleFor(x => x.RepairType).GreaterThan(0).WithMessage("补卡类型不能为空");
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator(validate);
    }
}
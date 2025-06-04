/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 FluentValidation 核心
using FluentValidation;

#endregion 引入命名空间

namespace ViteCent.Basic.Data.RepairSchedule;

/// <summary>
/// 补卡申请验证器
/// </summary>
[Serializable]
public partial class RepairScheduleValidator : AbstractValidator<AddRepairScheduleArgs>
{
    /// <summary>
    /// 验证补卡申请
    /// </summary>
    /// <param name="validate">是否验证</param>
    public RepairScheduleValidator(bool validate = false)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");

        // 验证部门标识不能为空
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("部门标识不能为空");

        // 验证补卡时间不能为空
        RuleFor(x => x.RepairTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("补卡时间不能为空");

        // 验证补卡类型不能为空
        RuleFor(x => x.RepairType).GreaterThan(0).WithMessage("补卡类型不能为空");

        // 验证排班标识不能为空
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");

        // 验证用户标识不能为空
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}
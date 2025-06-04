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

namespace ViteCent.Basic.Data.ShiftSchedule;

/// <summary>
/// 换班申请验证器
/// </summary>
[Serializable]
public partial class ShiftScheduleValidator : AbstractValidator<AddShiftScheduleArgs>
{
    /// <summary>
    /// 验证换班申请
    /// </summary>
    /// <param name="validate">是否验证</param>
    public ShiftScheduleValidator(bool validate = false)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");

        // 验证部门标识不能为空
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("部门标识不能为空");

        // 验证岗位标识不能为空
        RuleFor(x => x.PostId).NotNull().NotEmpty().WithMessage("岗位标识不能为空");

        // 验证排班标识不能为空
        RuleFor(x => x.ScheduleId).NotNull().NotEmpty().WithMessage("排班标识不能为空");

        // 验证换班部门标识不能为空
        RuleFor(x => x.ShiftDepartmentId).NotNull().NotEmpty().WithMessage("换班部门标识不能为空");

        // 验证换班岗位标识不能为空
        RuleFor(x => x.ShiftPostId).NotNull().NotEmpty().WithMessage("换班岗位标识不能为空");

        // 验证换班班次标识不能为空
        RuleFor(x => x.ShiftTypeId).NotNull().NotEmpty().WithMessage("换班班次标识不能为空");

        // 验证换班班次名称不能为空
        RuleFor(x => x.ShiftTypeName).NotNull().NotEmpty().WithMessage("换班班次名称不能为空");

        // 验证换班用户标识不能为空
        RuleFor(x => x.ShiftUserId).NotNull().NotEmpty().WithMessage("换班用户标识不能为空");

        // 验证班次标识不能为空
        RuleFor(x => x.TypeId).NotNull().NotEmpty().WithMessage("班次标识不能为空");

        // 验证班次名称不能为空
        RuleFor(x => x.TypeName).NotNull().NotEmpty().WithMessage("班次名称不能为空");

        // 验证用户标识不能为空
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}
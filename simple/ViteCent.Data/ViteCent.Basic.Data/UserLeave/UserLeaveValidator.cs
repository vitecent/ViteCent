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

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// 请假申请验证器
/// </summary>
[Serializable]
public partial class UserLeaveValidator : AbstractValidator<AddUserLeaveArgs>
{
    /// <summary>
    /// 验证请假申请
    /// </summary>
    /// <param name="validate">是否验证</param>
    public UserLeaveValidator(bool validate = true)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().When(x => validate).WithMessage("公司标识不能为空");

        // 验证部门标识不能为空
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().When(x => validate).WithMessage("部门标识不能为空");

        // 验证结束时间不能为空
        RuleFor(x => x.EndTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("结束时间不能为空");

        // 验证开始时间不能为空
        RuleFor(x => x.StartTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("开始时间不能为空");

        // 验证用户标识不能为空
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}
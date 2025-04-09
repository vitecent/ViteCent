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

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// 请假申请验证器
/// </summary>
[Serializable]
public partial class UserLeaveValidator : AbstractValidator<AddUserLeaveArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="validate"></param>
    public UserLeaveValidator(bool validate = false)
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.EndTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("结束时间不能为空");
        RuleFor(x => x.StartTime).Must(x => x > DateTime.MinValue && x < DateTime.MaxValue).WithMessage("开始时间不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator(validate);
    }
}
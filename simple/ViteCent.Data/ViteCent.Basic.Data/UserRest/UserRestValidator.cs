#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// 调休申请验证器
/// </summary>
[Serializable]
public partial class UserRestValidator : AbstractValidator<AddUserRestArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public UserRestValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.EndTime).NotNull().NotEmpty().WithMessage("结束时间不能为空");
        RuleFor(x => x.StartTime).NotNull().NotEmpty().WithMessage("开始时间不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator();
    }
}
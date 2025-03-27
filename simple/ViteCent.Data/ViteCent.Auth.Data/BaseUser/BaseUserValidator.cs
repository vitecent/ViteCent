#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseUserValidator : AbstractValidator<AddBaseUserArgs>
{
    /// <summary>
    /// </summary>
    public BaseUserValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("密码不能为空");
        RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("用户名不能为空");
        OverrideValidator();
    }
}
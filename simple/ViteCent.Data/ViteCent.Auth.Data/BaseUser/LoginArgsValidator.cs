#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public partial class LoginArgsValidator : AbstractValidator<LoginArgs>
{
    /// <summary>
    /// </summary>
    public LoginArgsValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("用户名不能为空");
        RuleFor(x => x.Username).Length(4, 12).WithMessage("用户名4-12个字符");
        RuleFor(x => x.Username).Matches(Const.PositiveEnglish).WithMessage("用户名只支持数字、字母");

        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("密码不能为空");
        RuleFor(x => x.Password).Length(6, 16).WithMessage("密码6-16个字符");
        RuleFor(x => x.Password).Matches(Const.PositiveEnglishUnderline).WithMessage("密码只支持数字、字母、下划线");
    }
}
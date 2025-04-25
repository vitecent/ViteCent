#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class ResetPaswordValidator : AbstractValidator<ResetPaswordArgs>
{
    /// <summary>
    /// </summary>
    public ResetPaswordValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("密码不能为空");
        RuleFor(x => x.Password).Length(6, 16).WithMessage("密码6-16个字符");
        RuleFor(x => x.Password).Matches(BaseConst.PositiveEnglishUnderline).WithMessage("密码只支持数字、字母、下划线");
    }
}
#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class ChangePaswordValidator : AbstractValidator<ChangePaswordArgs>
{
    /// <summary>
    /// </summary>
    public ChangePaswordValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.OriginalPassword).NotNull().NotEmpty().WithMessage("原密码不能为空");
        RuleFor(x => x.OriginalPassword).Length(6, 16).WithMessage("原密码6-16个字符");
        RuleFor(x => x.OriginalPassword).Matches(BaseConst.PositiveEnglishUnderline).WithMessage("原密码只支持数字、字母、下划线");

        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("新密码不能为空");
        RuleFor(x => x.Password).Length(6, 16).WithMessage("新密码6-16个字符");
        RuleFor(x => x.Password).Matches(BaseConst.PositiveEnglishUnderline).WithMessage("新密码只支持数字、字母、下划线");

        RuleFor(x => x).Must(x => x.Password != x.OriginalPassword).WithMessage("新密码不能和原始密码相同");
    }
}
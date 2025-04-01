#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
public partial class BaseCompanyValidator : AbstractValidator<AddBaseCompanyArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("编码不能为空");
        RuleFor(x => x.Code).Length(1, 50).WithMessage("编码1-50个字符");
        RuleFor(x => x.Code).Matches(Const.PositiveEnglishUnderline).WithMessage("编码只支持数字、字母、下划线");

        RuleFor(x => x.Name).Length(1, 50).WithMessage("名称1-50个字符");
        RuleFor(x => x.Name).Matches(Const.PositiveChineseEnglishUnderline).WithMessage("名称只支持数字、字母、中文、下划线");

        RuleFor(x => x.LegalPhone).Matches(Const.Mobile).When(x => !string.IsNullOrWhiteSpace(x.LegalPhone)).WithMessage("电话格式错误");

        RuleFor(x => x.Email).Matches(Const.Email).When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("邮箱格式错误");
    }
}
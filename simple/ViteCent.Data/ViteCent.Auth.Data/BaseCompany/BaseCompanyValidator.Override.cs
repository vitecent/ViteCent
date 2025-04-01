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
        RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("���벻��Ϊ��");
        RuleFor(x => x.Code).Length(1, 50).WithMessage("����1-50���ַ�");
        RuleFor(x => x.Code).Matches(Const.PositiveEnglishUnderline).WithMessage("����ֻ֧�����֡���ĸ���»���");

        RuleFor(x => x.Name).Length(1, 50).WithMessage("����1-50���ַ�");
        RuleFor(x => x.Name).Matches(Const.PositiveChineseEnglishUnderline).WithMessage("����ֻ֧�����֡���ĸ�����ġ��»���");

        RuleFor(x => x.LegalPhone).Matches(Const.Mobile).When(x => !string.IsNullOrWhiteSpace(x.LegalPhone)).WithMessage("�绰��ʽ����");

        RuleFor(x => x.Email).Matches(Const.Email).When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("�����ʽ����");
    }
}
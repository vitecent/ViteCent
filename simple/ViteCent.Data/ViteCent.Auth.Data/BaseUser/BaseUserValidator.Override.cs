#region

using FluentValidation;
using ViteCent.Core;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public partial class BaseUserValidator : AbstractValidator<AddBaseUserArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
        RuleFor(x => x.Username).Length(4, 12).WithMessage("�û���4-12���ַ�");
        RuleFor(x => x.Username).Matches(Const.PositiveEnglish).WithMessage("�û���ֻ֧�����֡���ĸ");

        RuleFor(x => x.Password).Length(6, 16).WithMessage("����6-16���ַ�");
        RuleFor(x => x.Password).Matches(Const.PositiveEnglishUnderline).WithMessage("����ֻ֧�����֡���ĸ���»���");

        RuleFor(x => x.Email).Matches(Const.Email).When(x => !string.IsNullOrWhiteSpace(x.Email)).WithMessage("�����ʽ����");

        RuleFor(x => x.IdCard).Must(x => x.IsIdCard()).When(x => !string.IsNullOrWhiteSpace(x.IdCard)).WithMessage("���֤�Ÿ�ʽ����");
        RuleFor(x => x.Birthday).Must(x => x < DateTime.Now && x > DateTime.MinValue).When(x => x.Birthday.HasValue).WithMessage("���ո�ʽ����");
        RuleFor(x => x).Must(x => x.IdCard.GetIdCardBirthday() == x.Birthday?.ToString("yyyy-MM-dd"))
            .When(x => !string.IsNullOrWhiteSpace(x.IdCard) && x.Birthday.HasValue).WithMessage("���֤�źͳ������ڲ�ƥ��");

        RuleFor(x => x.Phone).Matches(Const.Mobile).When(x => !string.IsNullOrWhiteSpace(x.Phone)).WithMessage("�绰��ʽ����");

        var genders = new List<int> { (int)GenderEnum.Male, (int)GenderEnum.FeMale };
        RuleFor(x => x.Gender).Must(x => genders.Contains(x)).When(x => !string.IsNullOrWhiteSpace(x.Phone)).WithMessage("�Ա��ʽ����");
    }
}
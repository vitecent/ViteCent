#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// </summary>
public partial class BaseDepartmentValidator : AbstractValidator<AddBaseDepartmentArgs>
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

        RuleFor(x => x.ManagerPhone).Matches(Const.Mobile).When(x => !string.IsNullOrWhiteSpace(x.ManagerPhone)).WithMessage("�绰��ʽ����");
    }
}
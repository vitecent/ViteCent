#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// </summary>
public partial class BaseSystemValidator : AbstractValidator<AddBaseSystemArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("���벻��Ϊ��");
        RuleFor(x => x.Code).Length(1, 50).WithMessage("����1-50���ַ�");
        RuleFor(x => x.Code).Matches(Const.PositiveEnglishUnderline).WithMessage("����ֻ֧�����֡���ĸ���»���");

        RuleFor(x => x.Name).Length(1, 50).WithMessage("����1-50���ַ�");
        RuleFor(x => x.Name).Matches(Const.PositiveChineseEnglishUnderline).WithMessage("����ֻ֧�����֡���ĸ�����ġ��»���");
    }
}
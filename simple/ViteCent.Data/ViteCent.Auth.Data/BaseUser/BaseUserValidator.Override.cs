/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

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
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate = false)
    {
        RuleFor(x => x.Username).Length(4, 12).WithMessage("用户名4-12个字符");
        RuleFor(x => x.Username).Matches(BaseConst.PositiveEnglish).WithMessage("用户名只支持数字、字母");

        if (!validate)
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("密码不能为空");
            RuleFor(x => x.Password).Length(6, 16).WithMessage("密码6-16个字符");
            RuleFor(x => x.Password).Matches(BaseConst.PositiveEnglishUnderline).WithMessage("密码只支持数字、字母、下划线");
        }
        else
        {
            RuleFor(x => x.Password).NotNull().NotEmpty().When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("密码不能为空");
            RuleFor(x => x.Password).Length(6, 16).When(x => !string.IsNullOrWhiteSpace(x.Password))
                .WithMessage("密码6-16个字符");
            RuleFor(x => x.Password).Matches(BaseConst.PositiveEnglishUnderline)
                .When(x => !string.IsNullOrWhiteSpace(x.Password)).WithMessage("密码只支持数字、字母、下划线");
        }

        RuleFor(x => x.Email).Matches(BaseConst.Email).When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("邮箱格式错误");

        RuleFor(x => x.IdCard).Must(x => x.IsIdCard()).When(x => !string.IsNullOrWhiteSpace(x.IdCard))
            .WithMessage("身份证号格式错误");
        RuleFor(x => x.Birthday).Must(x => x < DateTime.Now && x > DateTime.MinValue).When(x => x.Birthday.HasValue)
            .WithMessage("生日格式错误");
        RuleFor(x => x).Must(x => x.IdCard.GetIdAsyncCardBirthday() == x.Birthday?.ToString("yyyy-MM-dd"))
            .When(x => !string.IsNullOrWhiteSpace(x.IdCard) && x.Birthday.HasValue).WithMessage("身份证号和出生日期不匹配");

        RuleFor(x => x.Phone).Matches(BaseConst.Mobile).When(x => !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("电话格式错误");

        var genders = new List<int> { (int)GenderEnum.Male, (int)GenderEnum.FeMale };
        RuleFor(x => x.Gender).Must(x => genders.Contains(x)).When(x => !string.IsNullOrWhiteSpace(x.Phone))
            .WithMessage("性别格式错误");

        var status = new List<int> { (int)StatusEnum.Enable, (int)StatusEnum.Disable };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
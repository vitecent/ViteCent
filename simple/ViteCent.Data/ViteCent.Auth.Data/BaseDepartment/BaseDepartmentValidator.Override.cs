/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// </summary>
public partial class BaseDepartmentValidator : AbstractValidator<AddBaseDepartmentArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate">是否验证</param>
    private void OverrideValidator(bool validate = false)
    {
        RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("编码不能为空");
        RuleFor(x => x.Code).Length(1, 50).WithMessage("编码1-50个字符");
        RuleFor(x => x.Code).Matches(BaseConst.PositiveEnglishUnderline).WithMessage("编码只支持数字、字母、下划线");

        RuleFor(x => x.Name).Length(1, 50).WithMessage("名称1-50个字符");
        RuleFor(x => x.Name).Matches(BaseConst.PositiveChineseEnglishUnderline).WithMessage("名称只支持数字、字母、中文、下划线");

        RuleFor(x => x.ManagerPhone).Matches(BaseConst.Mobile).When(x => !string.IsNullOrWhiteSpace(x.ManagerPhone))
            .WithMessage("电话格式错误");

        var status = new List<int> { (int)StatusEnum.Enable, (int)StatusEnum.Disable };

        RuleFor(x => x.Status).Must(x => status.Contains(x.Value)).When(x => x.Status.HasValue).WithMessage("状态不存在");
    }
}
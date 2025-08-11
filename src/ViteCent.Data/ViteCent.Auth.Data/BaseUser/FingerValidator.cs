#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public class FingerValidator : AbstractValidator<FingerArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate">是否验证</param>
    public FingerValidator(bool validate = true)
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.CompanyId).NotNull().NotEmpty().When(x => validate).WithMessage("公司标识不能为空");
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().When(x => validate).WithMessage("部门标识不能为空");
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("标识不能为空");
    }
}
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
    public FingerValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("部门标识不能为空");
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("标识不能为空");
    }
}
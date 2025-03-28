#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseDepartmentValidator : AbstractValidator<AddBaseDepartmentArgs>
{
    /// <summary>
    /// </summary>
    public BaseDepartmentValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
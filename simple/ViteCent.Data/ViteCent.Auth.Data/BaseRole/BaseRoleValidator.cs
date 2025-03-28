#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseRoleValidator : AbstractValidator<AddBaseRoleArgs>
{
    /// <summary>
    /// </summary>
    public BaseRoleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// </summary>
[Serializable]
public partial class BasePositionValidator : AbstractValidator<AddBasePositionArgs>
{
    /// <summary>
    /// </summary>
    public BasePositionValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
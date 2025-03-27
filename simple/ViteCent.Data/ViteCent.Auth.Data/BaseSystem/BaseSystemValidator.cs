#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseSystemValidator : AbstractValidator<AddBaseSystemArgs>
{
    /// <summary>
    /// </summary>
    public BaseSystemValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        OverrideValidator();
    }
}
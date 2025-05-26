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

        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("标识不能为空");
    }
}
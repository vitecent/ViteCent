#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class RefreshTokenArgsValidator : AbstractValidator<RefreshTokenArgs>
{
    /// <summary>
    /// </summary>
    public RefreshTokenArgsValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
    }
}
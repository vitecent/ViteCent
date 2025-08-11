#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
[Serializable]
public class RefreshTokenValidator : AbstractValidator<RefreshTokenArgs>
{
    /// <summary>
    /// </summary>
    public RefreshTokenValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
    }
}
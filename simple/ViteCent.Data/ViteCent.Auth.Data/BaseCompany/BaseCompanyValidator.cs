#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseCompanyValidator : AbstractValidator<AddBaseCompanyArgs>
{
    /// <summary>
    /// </summary>
    public BaseCompanyValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
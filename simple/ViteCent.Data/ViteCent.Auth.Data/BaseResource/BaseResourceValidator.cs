#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseResource;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseResourceValidator : AbstractValidator<AddBaseResourceArgs>
{
    /// <summary>
    /// </summary>
    public BaseResourceValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.SystemId).NotNull().NotEmpty().WithMessage("系统标识不能为空");

        OverrideValidator();
    }
}
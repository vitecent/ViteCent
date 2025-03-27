#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// </summary>
[Serializable]
public partial class BaseOperationValidator : AbstractValidator<AddBaseOperationArgs>
{
    /// <summary>
    /// </summary>
    public BaseOperationValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.ResourceId).NotNull().NotEmpty().WithMessage("系统标识不能为空");
        RuleFor(x => x.SystemId).NotNull().NotEmpty().WithMessage("系统标识不能为空");
        OverrideValidator();
    }
}
#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// 操作信息验证器
/// </summary>
[Serializable]
public partial class BaseOperationValidator : AbstractValidator<AddBaseOperationArgs>
{
    /// <summary>
    /// 验证参数
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
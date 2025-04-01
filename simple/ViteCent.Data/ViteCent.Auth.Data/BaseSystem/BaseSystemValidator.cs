#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// 系统信息验证器
/// </summary>
[Serializable]
public partial class BaseSystemValidator : AbstractValidator<AddBaseSystemArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public BaseSystemValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
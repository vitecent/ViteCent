/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// 角色信息验证器
/// </summary>
[Serializable]
public partial class BaseRoleValidator : AbstractValidator<AddBaseRoleArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public BaseRoleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");

        OverrideValidator();
    }
}
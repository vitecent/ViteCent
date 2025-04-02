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

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 用户角色验证器
/// </summary>
[Serializable]
public partial class BaseUserRoleValidator : AbstractValidator<AddBaseUserRoleArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public BaseUserRoleValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("角色标识不能为空");
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        OverrideValidator();
    }
}
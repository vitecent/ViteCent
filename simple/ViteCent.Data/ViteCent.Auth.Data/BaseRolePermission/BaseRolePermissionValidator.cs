#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// 角色权限验证器
/// </summary>
[Serializable]
public partial class BaseRolePermissionValidator : AbstractValidator<AddBaseRolePermissionArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public BaseRolePermissionValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.OperationId).NotNull().NotEmpty().WithMessage("操作标识不能为空");
        RuleFor(x => x.ResourceId).NotNull().NotEmpty().WithMessage("资源标识不能为空");
        RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("角色标识不能为空");
        RuleFor(x => x.SystemId).NotNull().NotEmpty().WithMessage("系统标识不能为空");

        OverrideValidator();
    }
}
/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region 引入命名空间

// 引入 FluentValidation 核心
using FluentValidation;

#endregion 引入命名空间

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// 角色权限验证器
/// </summary>
[Serializable]
public partial class BaseRolePermissionValidator : AbstractValidator<AddBaseRolePermissionArgs>
{
    /// <summary>
    /// 验证角色权限
    /// </summary>
    /// <param name="validate">是否验证</param>
    public BaseRolePermissionValidator(bool validate = false)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");

        // 验证操作标识不能为空
        RuleFor(x => x.OperationId).NotNull().NotEmpty().WithMessage("操作标识不能为空");

        // 验证资源标识不能为空
        RuleFor(x => x.ResourceId).NotNull().NotEmpty().WithMessage("资源标识不能为空");

        // 验证角色标识不能为空
        RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("角色标识不能为空");

        // 验证系统标识不能为空
        RuleFor(x => x.SystemId).NotNull().NotEmpty().WithMessage("系统标识不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}
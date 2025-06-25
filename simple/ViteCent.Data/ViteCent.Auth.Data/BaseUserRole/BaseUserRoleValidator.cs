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

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// 用户角色验证器
/// </summary>
[Serializable]
public partial class BaseUserRoleValidator : AbstractValidator<AddBaseUserRoleArgs>
{
    /// <summary>
    /// 验证用户角色
    /// </summary>
    /// <param name="validate">是否验证</param>
    public BaseUserRoleValidator(bool validate = true)
    {
        // 验证参数不能为空
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");

        // 验证公司标识不能为空
        RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("公司标识不能为空");

        // 验证部门标识不能为空
        RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("部门标识不能为空");

        // 验证角色标识不能为空
        RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("角色标识不能为空");

        // 验证用户标识不能为空
        RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("用户标识不能为空");

        // 调用扩展方法进行额外验证
        OverrideValidator(validate);
    }
}
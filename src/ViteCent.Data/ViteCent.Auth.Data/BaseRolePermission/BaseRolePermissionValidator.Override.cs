/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
public partial class BaseRolePermissionValidator : AbstractValidator<AddBaseRolePermissionArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate">是否验证</param>
    private void OverrideValidator(bool validate = true)
    {
        var status = new List<int> { (int)StatusEnum.Enable, (int)StatusEnum.Disable };

        RuleFor(x => x.Status).Must(x => status.Contains(x.Value)).When(x => x.Status.HasValue).WithMessage("状态不存在");
    }
}
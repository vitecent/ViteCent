/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// </summary>
public partial class BaseUserRoleValidator : AbstractValidator<AddBaseUserRoleArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate = false)
    {
        var status = new List<int>() { (int)StatusEnum.Enable, (int)StatusEnum.Disable };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
#region

using FluentValidation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Data.BaseRolePermission;

/// <summary>
/// </summary>
public partial class BaseRolePermissionValidator : AbstractValidator<AddBaseRolePermissionArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
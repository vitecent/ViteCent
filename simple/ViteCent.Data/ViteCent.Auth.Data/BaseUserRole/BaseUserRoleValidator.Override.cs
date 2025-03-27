#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUserRole;

/// <summary>
/// </summary>
public partial class BaseUserRoleValidator : AbstractValidator<AddBaseUserRoleArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
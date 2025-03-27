#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseRole;

/// <summary>
/// </summary>
public partial class BaseRoleValidator : AbstractValidator<AddBaseRoleArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
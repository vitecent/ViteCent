#region

using FluentValidation;
using ViteCent.Core.Data;

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
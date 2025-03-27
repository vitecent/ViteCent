#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseUser;

/// <summary>
/// </summary>
public partial class BaseUserValidator : AbstractValidator<AddBaseUserArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
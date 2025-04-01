#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.UserRest;

/// <summary>
/// </summary>
public partial class UserRestValidator : AbstractValidator<AddUserRestArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
    }
}
#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BasePosition;

/// <summary>
/// </summary>
public partial class BasePositionValidator : AbstractValidator<AddBasePositionArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
    }
}
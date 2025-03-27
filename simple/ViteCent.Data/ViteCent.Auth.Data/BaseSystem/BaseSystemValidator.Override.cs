#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseSystem;

/// <summary>
/// </summary>
public partial class BaseSystemValidator : AbstractValidator<AddBaseSystemArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
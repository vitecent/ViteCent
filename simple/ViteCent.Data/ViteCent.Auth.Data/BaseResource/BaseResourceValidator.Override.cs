#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseResource;

/// <summary>
/// </summary>
public partial class BaseResourceValidator : AbstractValidator<AddBaseResourceArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
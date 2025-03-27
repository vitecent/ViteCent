#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseCompany;

/// <summary>
/// </summary>
public partial class BaseCompanyValidator : AbstractValidator<AddBaseCompanyArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
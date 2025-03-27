#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseDepartment;

/// <summary>
/// </summary>
public partial class BaseDepartmentValidator : AbstractValidator<AddBaseDepartmentArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
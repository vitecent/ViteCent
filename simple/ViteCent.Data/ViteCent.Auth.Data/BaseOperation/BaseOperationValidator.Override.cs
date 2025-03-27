#region

using FluentValidation;

#endregion

namespace ViteCent.Auth.Data.BaseOperation;

/// <summary>
/// </summary>
public partial class BaseOperationValidator : AbstractValidator<AddBaseOperationArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
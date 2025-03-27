#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// </summary>
public partial class UserLeaveValidator : AbstractValidator<AddUserLeaveArgs>
{
    /// <summary>
    /// </summary>
    public void OverrideValidator()
    {
    }
}
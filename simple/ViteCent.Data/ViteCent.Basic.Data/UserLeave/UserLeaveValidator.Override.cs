/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using FluentValidation;
using ViteCent.Basic.Data.UserRest;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Data.UserLeave;

/// <summary>
/// </summary>
public partial class UserLeaveValidator : AbstractValidator<AddUserLeaveArgs>
{
    /// <summary>
    /// </summary>
    private void OverrideValidator()
    {
        var status = new List<int>() { (int)UserLeaveEnum.Apply, (int)UserLeaveEnum.Pass, (int)UserLeaveEnum.NoPass };

        RuleFor(x => x.Status).Must(x => status.Contains(x)).WithMessage("状态不存在");
    }
}
/*
 * **********************************
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 * **********************************
 */

#region

using FluentValidation;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Basic.Data.BasePost;

/// <summary>
/// 验证职位信息拓展
/// </summary>
public partial class BasePostValidator : AbstractValidator<AddBasePostArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    /// <param name="validate"></param>
    private void OverrideValidator(bool validate = false)
    {
        var status = new List<int> { (int)StatusEnum.Enable, (int)StatusEnum.Disable };

        RuleFor(x => x.Status).Must(x => status.Contains(x.Value)).When(x => x.Status.HasValue).WithMessage("状态不存在");
    }
}
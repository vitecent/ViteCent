/*
 * **********************************
 * 代码由工具自动生成，请勿人工修改
 * 重新生成时，将覆盖原有代码
 * 如需扩展该类，请在partial类中实现
 * **********************************
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// 基础排班验证器
/// </summary>
[Serializable]
public partial class ScheduleTypeValidator : AbstractValidator<AddScheduleTypeArgs>
{
    /// <summary>
    /// 验证参数
    /// </summary>
    public ScheduleTypeValidator()
    {
        RuleFor(x => x).NotNull().WithMessage("参数不能为空");
        RuleFor(x => x.EndTime).NotNull().NotEmpty().WithMessage("结束时间不能为空");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("名称不能为空");
        RuleFor(x => x.Overnight).NotNull().NotEmpty().WithMessage("是否跨天不能为空");
        RuleFor(x => x.StartTime).NotNull().NotEmpty().WithMessage("开始时间不能为空");

        OverrideValidator();
    }
}
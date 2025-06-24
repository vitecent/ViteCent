/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using FluentValidation;

#endregion

namespace ViteCent.Basic.Data.ScheduleType;

/// <summary>
/// </summary>
public partial class ScheduleTypeValidator : AbstractValidator<AddScheduleTypeArgs>
{
    /// <summary>
    /// </summary>
    /// <param name="validate">是否验证</param>
    private void OverrideValidator(bool validate)
    {
        var scheduleTypes = new List<int>
            { (int)ScheduleTypeEnum.Schedule, (int)ScheduleTypeEnum.Leave, (int)ScheduleTypeEnum.Rest };
    }
}
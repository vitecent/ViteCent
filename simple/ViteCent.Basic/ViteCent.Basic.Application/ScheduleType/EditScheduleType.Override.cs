/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Basic.Entity.ScheduleType;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// </summary>
public partial class EditScheduleType
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(ScheduleTypeEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditScheduleTypeArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasScheduleTypeEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
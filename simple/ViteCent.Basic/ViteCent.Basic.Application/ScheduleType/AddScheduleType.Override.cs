/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Basic.Data.ScheduleType;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.ScheduleType;

/// <summary>
/// </summary>
public partial class AddScheduleType
{
    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddScheduleTypeArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasScheduleTypeEntityArgs
        {
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
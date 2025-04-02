/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Basic.Data.UserRest;
using ViteCent.Basic.Entity.UserRest;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Basic.Application.UserRest;

/// <summary>
/// </summary>
public partial class EditUserRest
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(UserRestEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditUserRestArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasUserRestEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            DepartmentId = request.DepartmentId,
            UserId = request.UserId,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
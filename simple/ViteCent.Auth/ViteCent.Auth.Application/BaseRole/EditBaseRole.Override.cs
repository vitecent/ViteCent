/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseRole;
using ViteCent.Auth.Entity.BaseRole;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseRole;

/// <summary>
/// </summary>
public partial class EditBaseRole
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseRoleEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseRoleArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseRoleEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */
 
#region

using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Entity.BaseCompany;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Auth.Entity.BaseUserRole;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseResource;

/// <summary>
/// </summary>
public partial class EditBaseResource
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseResourceEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult(string.Empty));
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseResourceArgs request, CancellationToken cancellationToken)
    {
        var hasArgs = new HasBaseResourceEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
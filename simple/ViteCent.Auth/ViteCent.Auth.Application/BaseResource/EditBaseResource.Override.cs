/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseResource;
using ViteCent.Auth.Entity.BaseResource;
using ViteCent.Core.Data;

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
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseResourceArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;
        else request.CompanyName = hasCompany?.Data?.Name ?? string.Empty;

        var hasSystem = await mediator.CheckSystem(request.CompanyId, request.SystemId);

        if (!hasSystem.Success)
            return hasSystem;
        else request.SystemName = hasSystem?.Data?.Name ?? string.Empty;

        var hasArgs = new HasBaseResourceEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
            Code = request.Code,
            Name = request.Name
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
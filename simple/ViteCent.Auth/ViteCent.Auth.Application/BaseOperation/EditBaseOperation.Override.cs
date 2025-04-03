/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Auth.Entity.BaseOperation;
using ViteCent.Core.Data;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// </summary>
public partial class EditBaseOperation
{
    /// <summary>
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(BaseOperationEntity entity, CancellationToken cancellationToken)
    {
        return await Task.FromResult(new BaseResult());
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(EditBaseOperationArgs request, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (hasCompany.Success)
            return hasCompany;

        var hasSystem = await mediator.CheckSystem(request.CompanyId, request.SystemId);

        if (hasSystem.Success)
            return hasSystem;

        var hasResource = await mediator.CheckResource(request.CompanyId, request.SystemId, request.ResourceId); ;

        if (hasResource.Success)
            return hasResource;

        var hasArgs = new HasBaseOperationEntityArgs
        {
            Id = request.Id,
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
            ResourceId = request.ResourceId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
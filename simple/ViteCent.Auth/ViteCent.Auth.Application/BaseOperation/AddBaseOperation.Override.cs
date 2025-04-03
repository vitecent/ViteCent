/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseOperation;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseOperation;

/// <summary>
/// </summary>
public partial class AddBaseOperation
{
    /// <summary>
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseOperationListArgs request, BaseUserInfo user)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
        {
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;
        }

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var systemIds = request.Items.Select(x => x.SystemId).Distinct().ToList();
        var resourceIds = request.Items.Select(x => x.ResourceId).Distinct().ToList();

        var companys = await mediator.CheckCompany(companyIds);

        if (!companys.Success)
            return companys;

        var systems = await mediator.CheckSystem(companyIds, systemIds);

        if (!systems.Success)
            return systems;

        var resources = await mediator.CheckResource(companyIds, systemIds, resourceIds);

        if (!resources.Success)
            return resources;

        return new BaseResult();
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async Task<BaseResult> OverrideHandle(AddBaseOperationArgs request, CancellationToken cancellationToken)
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
            CompanyId = request.CompanyId,
            SystemId = request.SystemId,
            ResourceId = request.ResourceId,
            Code = request.Code,
            Name = request.Name,
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}
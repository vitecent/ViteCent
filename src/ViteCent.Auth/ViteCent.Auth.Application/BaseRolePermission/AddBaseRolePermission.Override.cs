/*
 * 代码由工具自动生成
 * 重新生成时，不会覆盖原有代码
 */

#region

using MediatR;
using ViteCent.Auth.Data.BaseRolePermission;
using ViteCent.Auth.Entity.BaseRolePermission;
using ViteCent.Core.Data;
using ViteCent.Core.Enums;

#endregion

namespace ViteCent.Auth.Application.BaseRolePermission;

/// <summary>
/// </summary>
public partial class AddBaseRolePermission
{
    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="request">请求参数</param>
    /// <param name="user">用户信息</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task<BaseResult> OverrideHandle(IMediator mediator, AddBaseRolePermissionListArgs request,
        BaseUserInfo user, CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        foreach (var item in request.Items)
            if (string.IsNullOrWhiteSpace(item.CompanyId))
                item.CompanyId = companyId;

        var companyIds = request.Items.Select(x => x.CompanyId).Distinct().ToList();
        var roleIds = request.Items.Select(x => x.RoleId).Distinct().ToList();
        var systemIds = request.Items.Select(x => x.SystemId).Distinct().ToList();
        var resourceIds = request.Items.Select(x => x.ResourceId).Distinct().ToList();
        var operationIds = request.Items.Select(x => x.OperationId).Distinct().ToList();

        var companys = await mediator.CheckCompanys(companyIds);

        if (!companys.Success)
            return companys;

        var roles = await mediator.CheckRoles(companyIds, roleIds);

        if (!roles.Success)
            return roles;

        var systems = await mediator.CheckSystems(companyIds, systemIds);

        if (!systems.Success)
            return systems;

        var resources = await mediator.CheckResources(companyIds, systemIds, resourceIds);

        if (!resources.Success)
            return resources;

        var operations = await mediator.CheckOperations(companyIds, systemIds, resourceIds, operationIds);

        if (!operations.Success)
            return operations;

        var hasListArgs = new HasBaseRolePermissionEntityListArgs
        {
            CompanyIds = companyIds,
            RoleIds = roleIds,
            SystemIds = systemIds,
            ResourceIds = resourceIds,
            OperationIds = operationIds
        };

        return await mediator.Send(hasListArgs, cancellationToken);
    }

    /// <summary>
    /// </summary>
    /// <param name="mediator">中介者，用于发送查询请求</param>
    /// <param name="topic">话题</param>
    /// <param name="entity">数据库模型</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    internal static async Task OverrideTopic(IMediator mediator, TopicEnum topic, BaseRolePermissionEntity entity,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    /// <summary>
    /// </summary>
    /// <param name="request">请求参数</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>处理结果</returns>
    private async Task<BaseResult> OverrideHandle(AddBaseRolePermissionArgs request,
        CancellationToken cancellationToken)
    {
        var companyId = user?.Company?.Id ?? string.Empty;

        if (string.IsNullOrWhiteSpace(request.CompanyId))
            request.CompanyId = companyId;

        var hasCompany = await mediator.CheckCompany(request.CompanyId);

        if (!hasCompany.Success)
            return hasCompany;

        var hasRole = await mediator.CheckRole(request.CompanyId, request.RoleId);

        if (!hasRole.Success)
            return hasRole;

        var hasSystem = await mediator.CheckSystem(request.CompanyId, request.SystemId);

        if (!hasSystem.Success)
            return hasSystem;

        var hasResource = await mediator.CheckResource(request.CompanyId, request.SystemId, request.ResourceId);

        if (!hasResource.Success)
            return hasResource;

        var hasOperation = await mediator.CheckOperation(request.CompanyId, request.SystemId, request.ResourceId,
            request.OperationId);

        if (!hasOperation.Success)
            return hasOperation;

        var hasArgs = new HasBaseRolePermissionEntityArgs
        {
            CompanyId = request.CompanyId,
            RoleId = request.RoleId,
            SystemId = request.SystemId,
            ResourceId = request.ResourceId,
            OperationId = request.OperationId
        };

        return await mediator.Send(hasArgs, cancellationToken);
    }
}